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
        public async Task<List<CountryCodeModel>> getallcountrycodes()
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

        public async Task<List<RegisterModel>> Getallusers()
        {
            try
            {
                var result = await _dbcontext.Registration.ToListAsync();
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

        public async Task<RegisterModel> GetuserbyEmail(string email)
        {
            try
            {

                var users = await _dbcontext.Registration.FirstOrDefaultAsync(x => x.Email == email);
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
