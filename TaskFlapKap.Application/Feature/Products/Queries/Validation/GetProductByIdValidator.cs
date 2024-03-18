using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feather.Products.Queries.Validation
{
	public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public GetProductByIdValidator(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			this.dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		private void CustomValidation()
		{
			RuleFor(p => p.id)
				.MustAsync(async (s, CancellationToken) => await dbContext.Products.IsAnyExistAsync(p => p.Id == s)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
