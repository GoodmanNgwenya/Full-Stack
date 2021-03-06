using Fullstack.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fullstack.ViewModels
{
  public class AdvertModel
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string AdvertHeadlineText { get; set; }
    [Required]
    public string Province { get; set; }
    [Required]
    public string City { get; set; }
   
    [Column(TypeName = "money")]
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string AdvertDetails { get; set; }
    public string ReleaseDate { get; set; }
    public byte[] ImageUrl { get; set; }
    public string AdvertStatus { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
  }
}
