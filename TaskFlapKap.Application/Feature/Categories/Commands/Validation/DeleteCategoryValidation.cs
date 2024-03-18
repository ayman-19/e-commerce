using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Validation
{
	public class DeleteCategoryValidation : AbstractValidator<DeleteCategoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public DeleteCategoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			_dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(c => c.catId)
				.MustAsync(async (invId, CancellationToken) => await _dbContext.categories.IsAnyExistAsync(c => c.Id == invId)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
