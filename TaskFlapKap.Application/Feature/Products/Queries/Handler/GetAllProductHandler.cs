using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Application.Pagination;
using TaskFlapKap.DataTransfareObject.Product;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Feather.Products.Queries.Handler
{
	public class GetAllProductHandler : IRequestHandler<GatAllProductRequest, ResultPaginate<ProductResponse>>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<GetAllProductHandler> logger;
		private readonly IMapper mapper;

		public GetAllProductHandler(IUnitOfWork dbContext, ILogger<GetAllProductHandler> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;
		}
		public async Task<ResultPaginate<ProductResponse>> Handle(GatAllProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				logger.LogInformation("Get All Product!");
				Expression<Func<Product, ProductResponse>> expression = p => new ProductResponse
				{
					AmountAvailable = p.AmountAvailable,
					Cost = p.Cost,
					Id = p.Id,
					ProductName = p.Localize(p.ArabicName, p.EnglishName),
					SellerId = p.SellerId,
					SellerName = p.User!.UserName!
				};
				var result = dbContext.Products.GetPaginated(request.dto.Search, ["User"]);
				ResultPaginate<ProductResponse>? paginated = await result.Select(expression).ToPaginated(request.dto.PageNumber, request.dto.PageSize);
				return paginated;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return new ResultPaginate<ProductResponse>(new List<ProductResponse>(), 0, 0, 0);
			}
		}
	}
}
