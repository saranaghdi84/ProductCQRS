using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.UseCases.Product.Queries.GetProductsById;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, Result<ProductDto>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByIdHandler(IReadUnitOfWork readUnitOfWork, IMapper mapper)
    {
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<ProductDto>> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _readUnitOfWork.ProductReadRepository.GetByIdAsync(request.Id,cancellationToken);

        if (product == null)
            return Result.Failure<ProductDto>(Error.NotFound("Product", request.Id));

        var productDto = _mapper.Map<ProductDto>(product);
        return Result.Success(productDto);
    }
}
