using Ecommerce_api.Models;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface ISharedRepo
    {
        Task<List<CountryCode>> getallcountrycodes();
        Task<List<Users>> Getallusers();
        Task<Users> GetuserbyEmail(string email);
        Task<List<Roles>> GetallRoles();
        Task<List<States>> getallstates( int countryid);
        Task<List<Districts>> getalldistricts(int stateid);
    }
}
