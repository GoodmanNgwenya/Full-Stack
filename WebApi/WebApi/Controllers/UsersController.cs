using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
      var response = _userService.Authenticate(model);

      if (response == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {

      var users = _userService.GetAll();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _userService.GetById(id);
      return Ok(user);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
      var user = Map(model);
      try
      {
        // create user
        _userService.CreateUser(user, model.Password);
        return Ok();
      }
      catch (Exception ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateAdvertModel model)
    {
      var user = MapUser(model);
      user.Id = id;

      try
      {
        _userService.Update(user,model.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }


    private User Map(RegisterModel model)
    {
      return new User
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Username = model.Username,
        Role = model.Role
      };
    }
    private User MapUser(UpdateAdvertModel model)
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


  }
}
