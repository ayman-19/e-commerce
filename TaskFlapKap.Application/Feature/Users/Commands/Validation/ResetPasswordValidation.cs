using FluentValidation;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feature.Users.Commands.Validation
{
	internal class ResetPasswordValidation : AbstractValidator<ResetPasswordRequest>
	{
		private readonly IUnitOfWork dbContext;

		public ResetPasswordValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(u => u.dto.Email)
				.NotEmpty().WithMessage("Email Is Not Empty")
				.NotNull().WithMessage("Email Is Not Null")
				.MustAsync(async (m, CancellationToken) => await dbContext.Users.IsAnyExistAsync(s => s.Email!.Equals(m))).WithMessage("User Is Not Exist");


			RuleFor(u => u.dto.NewPassword)
			.NotEmpty().WithMessage("Password Is Not Empty")
			.NotNull().WithMessage("Password Is Not Null");

			RuleFor(u => u.dto.ConfirmNewPassword)
			.NotEmpty().WithMessage("ConfirmPassword Is Not Empty")
			.NotNull().WithMessage("ConfirmPassword Is Not Null");

			RuleFor(u => u.dto.NewPassword)
				.Equal(u => u.dto.ConfirmNewPassword)
				.WithMessage("ConfirmPassword Not Equal Password");

			RuleFor(u => u.dto.Code)
			.NotEmpty().WithMessage("Code Is Not Empty")
			.NotNull().WithMessage("Code Is Not Null");

		}
	}
}
