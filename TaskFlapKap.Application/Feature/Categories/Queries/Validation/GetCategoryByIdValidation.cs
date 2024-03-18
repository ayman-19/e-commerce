using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Categories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Categories.Queries.Validation
{
	public class GetCategoryByIdValidation : AbstractValidator<GetCategoryByIdRequest>
	{
		private readonly IStringLocalizer<Sources> _stringLocalizer;
		private readonly IUnitOfWork _dbContext;
		public GetCategoryByIdValidation(IStringLocalizer<Sources> stringLocalizer, IUnitOfWork dbContext)
		{
			_stringLocalizer = stringLocalizer;
			_dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(c => c.catId)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.MustAsync(async (id, CancellationToken) => await _dbContext.categories.IsAnyExistAsync(c => c.Id == id)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
