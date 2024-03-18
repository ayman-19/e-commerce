using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Validation
{
	public class AddInventoryValidation : AbstractValidator<AddInventoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public AddInventoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			_dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(inv => inv.command.EnglishName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.MustAsync(async (eng, CancellationToken) => !await _dbContext.Inventories.IsAnyExistAsync(inv => inv.EnglishName == eng)).WithMessage($"English Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(inv => inv.command.ArabicName)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.MustAsync(async (arb, CancellationToken) => !await _dbContext.Inventories.IsAnyExistAsync(inv => inv.ArabicName == arb)).WithMessage($"Arabic Name {_stringLocalizer[SourcesKey.Exist]}");

			RuleFor(inv => inv.command.Discription)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

		}
	}
}
