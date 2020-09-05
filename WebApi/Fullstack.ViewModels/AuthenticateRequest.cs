using System.ComponentModel.DataAnnotations;

namespace Fullstack.ViewModels
{
  public class AuthenticateRequest
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
