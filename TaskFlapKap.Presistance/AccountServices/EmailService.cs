using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TaskFlapKap.Application.IAccountServices;

namespace TaskFlapKap.Presistance.AccountServices
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> SendEmailAsync(string email, string message)
		{
			try
			{
				using (var smtpClient = new SmtpClient())
				{
					await smtpClient.ConnectAsync(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]!), true);
					//465
					await smtpClient.AuthenticateAsync(_configuration["Email:gmail"], _configuration["Email:password"]);
					var bodyBuilder = new BodyBuilder
					{
						HtmlBody = $"{message}",
						TextBody = "Wellcome"
					};
					var mailMessage = new MimeMessage()
					{
						Body = bodyBuilder.ToMessageBody()
					};
					mailMessage.From.Add(new MailboxAddress("Ayman Roshdy", _configuration["Email:gmail"]));
					mailMessage.To.Add(new MailboxAddress("Test", email));
					mailMessage.Subject = "Wellcome";
					await smtpClient.SendAsync(mailMessage);
					smtpClient.Disconnect(true);
				}
				return "Completed";
			}
			catch (Exception ex)
			{
				return "Failed";
			}
		}
	}
}
