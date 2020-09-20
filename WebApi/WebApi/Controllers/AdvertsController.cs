using Fullstack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AdvertsController : ControllerBase
  {
    private IAdvertService _advertService;
    public AdvertsController(IAdvertService advertService)
    {
      _advertService = advertService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {

      var adverts = _advertService.GetAllAdvert();
      return Ok(adverts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var advert = _advertService.GetAdvertById(id);
      return Ok(advert);
    }

    [Authorize]
    [HttpGet("{userId}/adverts")]
    public IActionResult GetAdvertsByUserId(int userId)
    {
      var user = HttpContext.Items["User"] as UserModel;
      if (user.Id != userId)
        return Forbid();

      var response = _advertService.GetAdvertsByUserId(userId);

      if (response == null)
        return BadRequest(new { message = "No avdert" });

      return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] AdvertModel model)
    {
    
      try
      {
        if(_advertService.IsValid(model))
        {
          _advertService.PostAdvert(model);
          return Ok(model);
        }
        else
        {
          return BadRequest();
        }

      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] AdvertModel model)
    {
      model.Id = id;

      try
      {
        if (_advertService.IsValid(model))
        {
          _advertService.UpdateAdvert(model);
          return Ok(model);
        }
        else
        {
          return BadRequest();
        }

      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      _advertService.Delete(id);
      return Ok();
    }

  }
}
