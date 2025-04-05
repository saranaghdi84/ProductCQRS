using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.UseCases.Product.Queries.GetProductsById;

public class GetProductsByIdHandler : IRequestHandler<GetProductsByIdQuery, Result<List<ProductDto>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByIdHandler(IReadUnitOfWork readUnitOfWork, IMapper mapper)
    {
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<List<ProductDto>>> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _readUnitOfWork.ProductReadRepository.GetByIdAsync(request.Id);
        if (product == null)
            return Result.Failure<List<ProductDto>>(Error.NotFound("Product", request.Id));
        return Result.Success(List<product>)
    }
}
