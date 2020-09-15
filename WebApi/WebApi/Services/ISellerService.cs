using Fullstack.ViewModels;
using System.Collections.Generic;

namespace WebApi.Services
{
  public interface ISellerService
  {
    List<SellerModel> GetAllSellers();
    SellerModel GetSellerByUserId(int userId);
    SellerModel UpdateSeller(SellerModel seller);
  }
}
