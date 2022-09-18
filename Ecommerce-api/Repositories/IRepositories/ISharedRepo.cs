using Ecommerce_api.Models;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface ISharedRepo
    {
        Task<List<CountryCodeModel>> getallcountrycodes();
        Task<List<RegisterModel>> Getallusers();
        Task<RegisterModel> GetuserbyEmail(string email);
    }
}
