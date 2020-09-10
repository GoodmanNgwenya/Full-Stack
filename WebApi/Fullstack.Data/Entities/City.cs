using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Entities
{
  public class City
  {
    public int Id { get; set; }
    public string CityName { get; set; }
    public int ProvinceId { get; set; }

  }
}
