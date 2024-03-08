namespace Ecommerce_api.Repositories.IRepositories
{
    public interface IMailServices
    {
        Task<int> SendEmail(string to, string username, string subject,string message);
    }
}
