using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fullstack.ViewModels
{
  public class RegisterModel
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

    public string Role { get; set; } = "User";
  }
}
