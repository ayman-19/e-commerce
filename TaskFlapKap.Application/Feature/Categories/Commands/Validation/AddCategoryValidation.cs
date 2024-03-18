using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Validation
{
	public class AddCategoryValidation : AbstractValidator<AddCategoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public AddCategoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
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

			RuleFor(c => c.Command)
						.MustAsync(async (name, CancellationToken) => !await _dbContext.CategoryInventories.NameIsExist(c => c.Category!.EnglishName == name.EnglishName && c.InventoryId == name.InventoryId)).WithMessage($"English Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(c => c.Command)
						.MustAsync(async (name, CancellationToken) => !await _dbContext.CategoryInventories.NameIsExist(c => c.Category!.ArabicName == name.ArabicName && c.InventoryId == name.InventoryId)).WithMessage($"Arbic Name {_stringLocalizer[SourcesKey.Exist]}");


			RuleFor(c => c.Command.ArabicName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);


			RuleFor(c => c.Command.Description)
			.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

			RuleFor(c => c.Command.InventoryId)
				.MustAsync(async (invId, CancellationToken) => await _dbContext.Inventories.IsAnyExistAsync(c => c.Id == invId)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}

	}
}
