using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.DbContexts;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{

  public class UserService : IUserService
  {
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    //private List<User> _users = new List<User>
    //{
    //    new User { Id = 1, FirstName = "Goodman", LastName = "Ngwenya", Username = "test", Password = "test" }
    //};
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


    public IEnumerable<User> GetAll()
    {
      //return _users;
      return _context.Users.ToList();
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

   
    public User GetById(int id)
    {
      // return _users.FirstOrDefault(x => x.Id == id);
      return _context.Users.FirstOrDefault(x => x.Id == id);
    }


    // helper methods

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
