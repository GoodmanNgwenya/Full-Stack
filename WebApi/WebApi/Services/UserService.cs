using Fullstack.Data.DbContexts;
using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;

namespace WebApi.Services
{

  public class UserService : IUserService
  {
    private readonly PropertyListingContext _context;

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings, PropertyListingContext context)
    {
      _appSettings = appSettings.Value;
      _context = context ?? throw new ArgumentNullException(nameof(context));

    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
      if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        return null;

      //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
      //var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
      var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

      // return null if user not found
      if (user == null) return null;

      // check if password is correct
      if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful so generate jwt token
      var token = generateJwtToken(user);

      return new AuthenticateResponse(user, token);
    }


    public IEnumerable<UserModel> GetAll()
    {
     // return _context.Users.ToList();
      var userList = _context.Users.ToList();
      return userList.Select(u => Map(u));
    }

    //retgister User
    public User CreateUser(User user,string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

      if (_context.Users.Any(x => x.Username == user.Username))
        throw new AppException("Username \"" + user.Username + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public UserModel GetById(int id)
    {
      var userEntity = _context.Users.FirstOrDefault(x => x.Id == id);
      if (userEntity == null) return null;

      return Map(userEntity);
    }

    // helper methods
    private UserModel Map(User user)
    {
      return new UserModel
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Username = user.Username,
        Role=user.Role
      };
    }
    private string generateJwtToken(User user)
    {
      // generate token that is valid for 7 days
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }


    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      if (passwordHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
      if (passwordSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i]) return false;
        }
      }

      return true;
    }

  }
}
