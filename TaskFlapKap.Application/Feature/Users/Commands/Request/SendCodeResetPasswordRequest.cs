using MediatR;

namespace TaskFlapKap.Application.Feature.Users.Commands.Request
{
	public record SendCodeResetPasswordRequest(string email) : IRequest<string>;
}
