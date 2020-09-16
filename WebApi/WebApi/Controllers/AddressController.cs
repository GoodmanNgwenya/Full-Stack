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
    private IAdvertService _advertService;
    public AddressController(IAdvertService advertService)
    {
      _advertService = advertService;
    }

    [HttpGet]
    public IActionResult GetProvince()
    {
      var province = _advertService.GetAllProvince();
      return Ok(province);
    }

    [HttpGet("city")]
    public IActionResult GetCity(CityModel cityModel)
    {
      var advert = _advertService.GetCities(cityModel.ProvinceId);
      return Ok(advert);
    }

    [HttpGet("{provinceId}/cities")]
    public IActionResult GetCitiesById(int provinceId)
    {
      var response = _advertService.GetCities(provinceId);

      if (response == null)
        return BadRequest(new { message = "No avdert" });

      return Ok(response);
    }

  }
}
