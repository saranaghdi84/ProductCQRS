using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;

namespace ProductCQRS.Application.UseCases.Product.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IReadUnitOfWork readUnitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    } 

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _readUnitOfWork.ProductReadRepository.GetByIdAsync(request.Id , cancellationToken);
        if (product == null)
        {
            return Result.Failure<bool>(Error.NotFound("Product", request.Id));
        }
        _mapper.Map<ProductCQRS.Domain.Entities.Product>(request);
        bool result =await  _unitOfWork.ProductRepository.UpdateAsync(product);
        if (!result) {
          return Result.Failure<bool>(Error.NotFound("Product", request.CategoryId));
        }
        return Result.Success<bool>(result);
    }
}
