namespace ShopAPP.Service.Layer.Services.EmailServices;
public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlmessage);
}