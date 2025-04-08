using MediatR;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.UseCases.ProductCategory.Queries.GetAllProducts;

public sealed record GetAllProductCategoriesQuery(int CategoryId) : IRequest<Result<IEnumerable<ProductCategoryDto>>>;
