namespace UserApi.Services;

public interface IMailServices
{
    Task SendEmailAsync(string email, string subject, string message);

}
