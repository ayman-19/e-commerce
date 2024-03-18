using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Handler;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IAccountServices;

namespace TaskFlapKap.Application.Feature.Users.Commands.Handler
{
	public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, string>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<UpdateUserHandler> logger;

		public ConfirmEmailHandler(IAccountService dbContext, ILogger<UpdateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
		{
			return await _dbContext.ConfirmEmailAsync(request.userId, request.code);
		}
	}
}
