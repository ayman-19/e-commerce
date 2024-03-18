namespace TaskFlapKap.Application.IAccountServices
{
	public interface IEmailService
	{
		Task<string> SendEmailAsync(string email, string message);
	}
}
