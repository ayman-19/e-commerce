using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Validation
{
	public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public UpdateCategoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			_dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(c => c.Command.EnglishName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

			RuleFor(c => c.Command.ArabicName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);


			RuleFor(c => c)
						.MustAsync(async (name, CancellationToken) => !await _dbContext.CategoryInventories.NameIsExist(c => c.Category!.Id != name.catId && c.Category.EnglishName == name.Command.EnglishName)).WithMessage($"English Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(c => c)
					.MustAsync(async (name, CancellationToken) => !await _dbContext.CategoryInventories.NameIsExist(c => c.Category!.Id != name.catId && c.Category.ArabicName == name.Command.ArabicName)).WithMessage($"Arabic Name {_stringLocalizer[SourcesKey.Exist]}");



			RuleFor(c => c.Command.Description)
			.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);
		}

	}
}
