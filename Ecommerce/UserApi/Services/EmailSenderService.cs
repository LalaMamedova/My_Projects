using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace UserApi.Services;

public class EmailSenderService:IMailServices
{
    IConfiguration _configuration;
    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.office365.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_configuration["Email:MyEmail"], _configuration["Email:Password"])
        };

        return client.SendMailAsync(
            new MailMessage(from: _configuration["Email:MyEmail"],
                            to: email,
                            subject,
                            message));
    }


}
