using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Inventories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Inventories.Queries.Validation
{
	public class GetInventoryByIdValidation : AbstractValidator<GetInventoryByIdRequest>
	{
		private readonly IStringLocalizer<Sources> _stringLocalizer;
		private readonly IUnitOfWork _dbContext;
		public GetInventoryByIdValidation(IStringLocalizer<Sources> stringLocalizer, IUnitOfWork dbContext)
		{
			_stringLocalizer = stringLocalizer;
			_dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(c => c.invId)
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
				.MustAsync(async (id, CancellationToken) => await _dbContext.Inventories.IsAnyExistAsync(c => c.Id == id)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
