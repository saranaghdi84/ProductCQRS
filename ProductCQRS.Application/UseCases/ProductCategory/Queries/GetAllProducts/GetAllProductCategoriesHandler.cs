using Mapster;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.UseCases.ProductCategory.Queries.GetAllProducts;

public class GetAllProductCategoriesHandler : IRequestHandler<GetAllProductCategoriesQuery, Result<IEnumerable<ProductCategoryDto>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductCategoriesHandler(IReadUnitOfWork readUnitOfWork, IUnitOfWork unitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<ProductCategoryDto>>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
    {


        var productCategories = await _readUnitOfWork.ProductCategoryReadRepository.GetAllAsync(cancellationToken);
        var categoriesWithProduct = new List<ProductCQRS.Domain.Entities.ProductCategory>();
        foreach (var productCategory in productCategories)
        {
            if (await _unitOfWork.ProductCategoryRepository.HasProductsAsync(request.CategoryId)) 
            {
                categoriesWithProduct.Add(productCategory);
            }
        }
        var result = categoriesWithProduct.Select(c=>c.Adapt<ProductCategoryDto>()).AsEnumerable();
        return Result.Success(result);


    }
}
