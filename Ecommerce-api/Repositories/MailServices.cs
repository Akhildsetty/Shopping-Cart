using Ecommerce_api.Repositories.IRepositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net;
using System.Net.Mail;

namespace Ecommerce_api.Repositories
{
    public class MailServices : IMailServices
    {
        private readonly IConfiguration _configuration;

        public MailServices(IConfiguration configuration) 
        {
            _configuration = configuration;
            
        }

        public async Task<int> SendEmail(string to,string username, string subject,string message)
        {
            using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient())
            {
                using (MailMessage mail = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(_configuration["SendInBlue:Senderfrom"]);


                    mail.From = fromAddress;
                    mail.Subject = subject;
                    // Set IsBodyHtml to true means you can send HTML email.
                    mail.IsBodyHtml = true;
                    mail.Body = message;
                    mail.To.Add(to);

                    var basicCredential = new NetworkCredential(_configuration["SmtpLogin"], _configuration["SmtpPassword"]);
                    smtpClient.Host = _configuration["SmtpHost"];
                    smtpClient.Port = int.Parse(_configuration["SmtpPort"]);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    try
                    {
                        smtpClient.SendAsync(mail, CancellationToken.None);
                        return 1;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
  
        }
    }
}
