using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Localization;

namespace TaskFlapKap.Application.Feather.Products.Commands.Validation
{
	public class CreateProductValidation : AbstractValidator<CreateProductRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IStringLocalizer<Sources> _stringLocalizer;

		public CreateProductValidation(IUnitOfWork dbContext, IHttpContextAccessor httpContext, IStringLocalizer<Sources> stringLocalizer)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			_stringLocalizer = stringLocalizer;
			CustomValidation();
		}
		private void CustomValidation()
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid)?.Value;

			RuleFor(p => _stringLocalizer[SourcesKey.NotEmpty])
				.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty]);

			RuleFor(p => p)
				.MustAsync(async (s, CancellationToken) => !await dbContext.Products.IsAnyExistAsync(p => p.ArabicName.Equals(s.Product.ArabicName) && p.EnglishName.Equals(s.Product.EnglishName))).WithMessage(_stringLocalizer[SourcesKey.NameExist]);

			RuleFor(p => p.Product.AmountAvailable)
			.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.GreaterThan(0).WithMessage(_stringLocalizer[SourcesKey.GreaterThanZero]);

			RuleFor(p => p.Product.Cost)
			.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
			.GreaterThan(0).WithMessage(_stringLocalizer[SourcesKey.GreaterThanZero]);


			RuleFor(p => userId)
				.MustAsync(async (s, CancellationToken) => await dbContext.Users.IsAnyExistAsync(p => p.Id.Equals(s))).WithMessage(_stringLocalizer[SourcesKey.UserNotExist]);

			RuleFor(p => p.Product.CategoryId)
					.NotNull().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
					.NotEmpty().WithMessage(_stringLocalizer[SourcesKey.NotEmpty])
					.MustAsync(async (catId, CancellationToken) => await dbContext.categories.IsAnyExistAsync(c => c.Id == catId)).WithMessage(_stringLocalizer[SourcesKey.NotExist]);
		}
	}
}
