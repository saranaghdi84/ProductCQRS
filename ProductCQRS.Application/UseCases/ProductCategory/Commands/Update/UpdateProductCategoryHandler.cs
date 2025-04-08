using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Application.UseCases.Product.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.ProductCategory.Commands.Update;

public class UpdateProductCategoryHandler : IRequestHandler<UpdateProductCategoryCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCategoryHandler(IUnitOfWork unitOfWork, IReadUnitOfWork readUnitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<bool>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _readUnitOfWork.ProductCategoryReadRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productCategory == null)
        {
            return Result.Failure<bool>(Error.NotFound("ProductCategory", request.Id));
        }
        _mapper.Map<ProductCQRS.Domain.Entities.ProductCategory>(request);
        bool result = await _unitOfWork.ProductCategoryRepository.UpdateAsync(productCategory);
        if (!result)
        {
            return Result.Failure<bool>(Error.NotFound("ProductCategory", request.Id));
        }
        return Result.Success<bool>(result);
    }
}
