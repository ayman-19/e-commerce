using FluentValidation;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feature.Users.Commands.Validation
{
	public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailRequest>
	{
		private readonly IUnitOfWork dbContext;

		public ConfirmEmailValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(u => u.userId)
				.NotEmpty().WithMessage("UserName Is Not Empty")
				.NotNull().WithMessage("UserName Is Not Null")
				.MustAsync(async (m, CancellationToken) => await dbContext.Users.IsAnyExistAsync(s => s.Id!.Equals(m))).WithMessage("User Is Not Exist");

			RuleFor(u => u.code)
			.NotEmpty().WithMessage("UserName Is Not Empty")
			.NotNull().WithMessage("UserName Is Not Null");
		}
	}
}
