using Fullstack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AddressController: ControllerBase
  {
    private IUserService _userService;
    public AddressController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public IActionResult GetProvince()
    {
      var province = _userService.GetAllProvince();
      return Ok(province);
    }

    [HttpGet("city")]
    public IActionResult GetCity(CityModel cityModel)
    {
      var advert = _userService.GetCities(cityModel.ProvinceId);
      return Ok(advert);
    }

    [HttpPost("city")]
    public IActionResult GetCitiesById(CityModel model)
    {
      var response = _userService.GetCities(model.ProvinceId);

      if (response == null)
        return BadRequest(new { message = "No avdert" });

      return Ok(response);
    }

  }
}
