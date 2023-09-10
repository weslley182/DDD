using AutoMapper;
using DDD.Application.Services.Interfaces;

namespace DDD.Application.Services;

public class EmailService : IEmailService
{
    private readonly IMapper _mapper;

    public EmailService(IMapper mapper)
    {
        _mapper = mapper;

    }
    public async Task SendEmailAsync(string body, string address)
    {
        Console.WriteLine($"Sending email to {address} with body: {body}");
    }
}
