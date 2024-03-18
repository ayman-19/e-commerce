using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Validation
{
	public class DeleteInventoryValidation : AbstractValidator<DeleteInventoryRequest>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public DeleteInventoryValidation(IUnitOfWork dbContext, IStringLocalizer<Sources> stringLocalizer)
		{
			_dbContext = dbContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(c => c.invId)
				.MustAsync(async (invId, CancellationToken) => await _dbContext.Inventories.IsAnyExistAsync(c => c.Id == invId)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
