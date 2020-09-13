using Fullstack.Data.DbContexts;
using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    private IFullStackRepository _repo;

    private readonly AppSettings _appSettings;
    public UserService(IOptions<AppSettings> appSettings, IFullStackRepository repo)
    {
      _appSettings = appSettings.Value;
      this._repo = repo;
    }
    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
      if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        return null;

      var user = _repo.GetUsers().SingleOrDefault(x => x.Username == model.Username);

      // return null if user not found
      if (user == null) return null;

      // check if password is correct
      if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful so generate jwt token
      var token = generateJwtToken(user);

      return new AuthenticateResponse(user, token);
    }


    public List<UserModel> GetAll()
    {
      var userList = _repo.GetUsers();
      return userList.Select(u => Map(u)).ToList();
    }

    //retgister User
    public User CreateUser(User user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

      if (_repo.GetUsers().Any(x => x.Username == user.Username))
        throw new AppException("Username \"" + user.Username + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _repo.CreateUser(user);

      return user;
    }

    //update user
    public void Update(User userParam,string Password=null)
    {
      //var user = _context.Users.Find(userParam.Id);
      var user = _repo.GetUser(userParam.Id);

      if (user == null)
        throw new AppException("User not found");

      // update username if it has changed
      if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
      {
        // throw error if the new username is already taken
        if (_repo.GetUsers().Any(x => x.Username == userParam.Username))
          throw new AppException("Username " + userParam.Username + " is already taken");

        user.Username = userParam.Username;
      }

      // update user properties if provided
      if (!string.IsNullOrWhiteSpace(userParam.FirstName))
        user.FirstName = userParam.FirstName;

      if (!string.IsNullOrWhiteSpace(userParam.LastName))
        user.LastName = userParam.LastName;

      if (!string.IsNullOrWhiteSpace(userParam.Username))
        user.Username = userParam.Username;

      if (!string.IsNullOrWhiteSpace(userParam.Role))
        user.Role = userParam.Role;

      // update password if provided
      if (!string.IsNullOrWhiteSpace(Password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(Password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _repo.UpdateUser(user);
    }


    public UserModel GetById(int id)
    {
      var userEntity = _repo.GetUser(id);
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
        Role = user.Role
      };
    }

    private User MapUser(UpdateUserModel model)
    {
      return new User
      {
        Id = model.Id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Username = model.Username,
        Role = model.Role
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
