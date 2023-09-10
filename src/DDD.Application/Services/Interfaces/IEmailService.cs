namespace DDD.Application.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string body, string address);
}
