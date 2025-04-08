using Mapster;
using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Application.UseCases.ProductCategory.Queries.GetAllProducts;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductDto>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IReadUnitOfWork readUnitOfWork, IUnitOfWork unitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _readUnitOfWork.ProductReadRepository.GetAllAsync(cancellationToken);
        var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Result.Success(productDto);
    }
}
