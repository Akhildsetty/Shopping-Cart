using Ecommerce_api.Data;
using Ecommerce_api.Models;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_api.Repositories
{
    public class SharedRepo : ISharedRepo
    {
        private readonly databaseContext _dbcontext;
        public SharedRepo(databaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<CountryCode>> getallcountrycodes()
        {
            try
            {
                var result = await _dbcontext.CountryCode.ToListAsync();
                if (result.Any())
                {
                    var sortedcodes=result.OrderBy(x=>x.Name).ToList();
                    return sortedcodes;
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Users>> Getallusers()
        {
            try
            {
                var result = await _dbcontext.Users.ToListAsync();
                return result;

                //using (var connection = _dapperContext.CreateConnection())
                //{
                //    string query = $"Select * from Registration";
                //    connection.Open();
                //    List<RegisterModel> result = (await connection.QueryAsync<RegisterModel>(query).ConfigureAwait(false)).ToList();
                //    return result;

                //}
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Users> GetuserbyEmail(string email)
        {
            try
            {

                var users = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == email);
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Roles>> GetallRoles()
        {
            try
            {
                var users = await _dbcontext.Roles.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<States>> getallstates(int countryid)
        {
            try
            {
                var result = await _dbcontext.States.Where(x=>x.Country==countryid).ToListAsync();
                if (result.Any())
                {
                    var sortedcodes = result.OrderBy(x => x.StateName).ToList();
                    return sortedcodes;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Districts>> getalldistricts(int stateid)
        {
            try
            {
                var result = await _dbcontext.Districts.Where(x=>x.StateCode==stateid).ToListAsync();
                if (result.Any())
                {
                    var sortedcodes = result.OrderBy(x => x.District).ToList();
                    return sortedcodes;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
