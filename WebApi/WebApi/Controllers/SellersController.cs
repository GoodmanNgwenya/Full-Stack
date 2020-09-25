using Fullstack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class SellersController:ControllerBase
  {
    private ISellerService _sellerService;
    public SellersController(ISellerService sellerService)
    {
      _sellerService = sellerService;
    }

    [HttpGet]
    public IActionResult GetAllSeller()
    {

      var seller = _sellerService.GetAllSellers();
      return Ok(seller);
    }

    [HttpGet("{id}")]
    public IActionResult GetSellerByUserId(int id)
    {
      var seller = _sellerService.GetSellerByUserId(id);
      return Ok(seller);
    }

    [HttpGet("{advertId}/seller")]
    public IActionResult GetSellerPerAdvert(int advertId)
    {
      var seller = _sellerService.GetSellerPerAdvert(advertId);
      return Ok(seller);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateSeller(int id, [FromBody] SellerModel model)
    {
      model.UserId = id;

      try
      {
        if (ModelState.IsValid)
        {
          _sellerService.UpdateSeller(model);
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
  }
}
