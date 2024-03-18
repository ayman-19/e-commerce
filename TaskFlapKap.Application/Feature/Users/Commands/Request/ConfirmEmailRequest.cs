using MediatR;

namespace TaskFlapKap.Application.Feature.Users.Commands.Request
{
	public record ConfirmEmailRequest(string userId, string code) : IRequest<string>;
}
