using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Queries.GetProductsByCategory;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByCategoryHandler(IReadUnitOfWork readUnitOfWork, IProductReadRepository productRepository, IMapper mapper)
    {
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _readUnitOfWork.ProductReadRepository.GetByCategoryAsync(request.CategoryId, cancellationToken);
        if (products == null) { return Result.Failure<IEnumerable<ProductDto>>(Error.NotFound("Product", request.CategoryId)); }
        var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Result.Success(productDto);

    }
}
