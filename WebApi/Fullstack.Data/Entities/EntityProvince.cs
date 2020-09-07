using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Entities
{
  public class EntityProvince
  {
    public int Id { get; set; }
    public string Province { get; set; }
    public ICollection<EntityCity> Cities { get; set; }
  }
}
