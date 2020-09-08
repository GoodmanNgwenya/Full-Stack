using Fullstack.Data.Entities;
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
    private IUserService _userService;
    public AdvertsController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {

      var adverts = _userService.GetAllAdvert();
      return Ok(adverts);
    }
    [HttpGet("{id}")]
    public IActionResult GetAdvertById(int id)
    {
      var advert = _userService.GetAdvertById(id);
      return Ok(advert);
    }

    [HttpPost]
    public IActionResult GetAdvertsById(AdvertModel model)
    {
      var response = _userService.GetAdvertsById(model.UserId);

      if (response == null)
        return BadRequest(new { message = "No avdert" });

      return Ok(response);
    }

    [HttpPost("addAdvert")]
    public IActionResult AddAdvert([FromBody] AdvertModel model)
    {
      // map model to entity and set id
      var advert = MapAdvert(model);

      try
      {
        if (ModelState.IsValid)
        {
          _userService.PostAdvert(advert);
          return Ok();
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


    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AdvertModel model)
    {
      // map model to entity and set id
      var advert = MapAdvert(model);
      advert.Id = id;

      try
      {
        if (ModelState.IsValid)
        {
          _userService.UpdateAdvert(advert);
          return Ok();
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
      _userService.Delete(id);
      return Ok();
    }

    private Advert MapAdvert(AdvertModel model)
    {
      return new Advert
      {
        AdvertHeadlineText = model.AdvertHeadlineText,
        AdvertDetails = model.AdvertDetails,
        Province = model.Province,
        City = model.City,
        Price = model.Price,
        UserId=model.UserId,
        ReleaseDate=model.ReleaseDate,
        AdvertStatus=model.AdvertStatus
      };
    }
  }
}
