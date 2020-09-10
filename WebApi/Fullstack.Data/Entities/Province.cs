using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Entities
{
  public class Province
  {
    public int Id { get; set; }
    public string ProvinceName { get; set; }
    public ICollection<City> Cities { get; set; }
  }
}
