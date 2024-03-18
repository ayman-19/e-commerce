using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Handler;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IAccountServices;

namespace TaskFlapKap.Application.Feature.Users.Commands.Handler
{
	public class RestPasswordHandler : IRequestHandler<ResetPasswordRequest, string>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<UpdateUserHandler> logger;

		public RestPasswordHandler(IAccountService dbContext, ILogger<UpdateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
		{
			var response = await _dbContext.ResetPasswordAsync(request.dto.Email, request.dto.NewPassword, request.dto.Code);
			return response;
		}
	}
}
