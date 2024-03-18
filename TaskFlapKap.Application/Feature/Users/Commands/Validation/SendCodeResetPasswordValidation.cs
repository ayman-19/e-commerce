using FluentValidation;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feature.Users.Commands.Validation
{
	internal class SendCodeResetPasswordValidation : AbstractValidator<SendCodeResetPasswordRequest>
	{
		private readonly IUnitOfWork dbContext;

		public SendCodeResetPasswordValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(u => u.email)
				.NotEmpty().WithMessage("UserName Is Not Empty")
				.NotNull().WithMessage("UserName Is Not Null")
				.MustAsync(async (m, CancellationToken) => await dbContext.Users.IsAnyExistAsync(s => s.Email!.Equals(m))).WithMessage("User Is Not Exist");

		}
	}
}
