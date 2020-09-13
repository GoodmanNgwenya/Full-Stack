using Fullstack.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.ViewModels
{
  public class AdvertModel
  {
    public int Id { get; set; }
    public string AdvertHeadlineText { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public decimal Price { get; set; }
    public string AdvertDetails { get; set; }
    public string ReleaseDate { get; set; }
    public byte[] ImageUrl { get; set; }
    public string AdvertStatus { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
  }
}
