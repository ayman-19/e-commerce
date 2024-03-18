using MediatR;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feature.Users.Commands.Request
{
	public record ResetPasswordRequest(ResetPassworddto dto) : IRequest<string>;
}
