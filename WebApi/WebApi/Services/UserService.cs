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
      //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
      var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

      // return null if user not found
      if (user == null) return null;

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
    public User CreateUser(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public User GetById(int id)
    {
      // return _users.FirstOrDefault(x => x.Id == id);
      return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public bool Save()
    {
      return (_context.SaveChanges() >= 0);
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
  }
}
