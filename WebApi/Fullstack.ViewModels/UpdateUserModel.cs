using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.ViewModels
{
  public class UpdateUserModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
  }
}
