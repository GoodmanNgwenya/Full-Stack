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
    public IActionResult GetAdvertById(int id)
    {
      var advert = _advertService.GetAdvertById(id);
      return Ok(advert);
    }

    [HttpPost]
    public IActionResult GetAdvertsById(AdvertModel model)
    {
      var response = _advertService.GetAdvertsById(model.UserId);

      if (response == null)
        return BadRequest(new { message = "No avdert" });

      return Ok(response);
    }

    [HttpPost("addAdvert")]
    public IActionResult AddAdvert([FromBody] AdvertModel model)
    {
      // map model to entity and set id
      //var advert = MapAdvert(model);

      try
      {
        if (ModelState.IsValid)
        {
          _advertService.PostAdvert(model);
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
      model.Id = id;

      try
      {
        if (ModelState.IsValid)
        {
          _advertService.UpdateAdvert(model);
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
      _advertService.Delete(id);
      return Ok();
    }

  }
}
