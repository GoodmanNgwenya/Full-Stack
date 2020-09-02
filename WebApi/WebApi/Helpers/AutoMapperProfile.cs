using AutoMapper;
using Fullstack.Data.Entities;
using WebApi.Models;

namespace WebApi.Helpers
{
  public class AutoMapperProfile:Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User,UserModel>();
      CreateMap<RegisterDto, User>();
    }
  
  }
}
