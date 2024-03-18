using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Handler;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IAccountServices;

namespace TaskFlapKap.Application.Feature.Users.Commands.Handler
{
	public class SendCodeResetPasswordHandler : IRequestHandler<SendCodeResetPasswordRequest, string>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<UpdateUserHandler> logger;

		public SendCodeResetPasswordHandler(IAccountService dbContext, ILogger<UpdateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(SendCodeResetPasswordRequest request, CancellationToken cancellationToken)
		{
			var response = await _dbContext.SendResetPasswordCodeAsync(request.email);
			return response;
		}
	}
}
