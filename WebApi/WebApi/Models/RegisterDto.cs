using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
  public class RegisterDto
  {
    [Required]
    [MinLength(1)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [MinLength(6)]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
  }
}
