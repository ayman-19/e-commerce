using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Validation
{
	public class UpdateInventoryValidation : AbstractValidator<UpdateInventoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public UpdateInventoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			_dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(inv => inv.Command.EnglishName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

			RuleFor(inv => inv)
				.MustAsync(async (eng, CancellationToken) => !await _dbContext.Inventories.IsAnyExistAsync(inv => inv.EnglishName == eng.Command.EnglishName && inv.Id != eng.id)).WithMessage($"English Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(inv => inv.Command.ArabicName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

			RuleFor(inv => inv)
			.MustAsync(async (eng, CancellationToken) => !await _dbContext.Inventories.IsAnyExistAsync(inv => inv.ArabicName == eng.Command.ArabicName && inv.Id != eng.id)).WithMessage($"Arabic Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(inv => inv.Command.Discription)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

		}
	}
}
