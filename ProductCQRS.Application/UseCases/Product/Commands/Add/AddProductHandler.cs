using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Commands.Add;

public class AddProductHandler : IRequestHandler<AddProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public AddProductHandler(IUnitOfWork unitOfWork,IReadUnitOfWork readUnitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<ProductCQRS.Domain.Entities.Product>(request);
        var category = await _readUnitOfWork.ProductCategoryReadRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
        {
            return Result.Failure<bool>(Error.NotFound("ProductCategory", request.CategoryId));
        }

        bool result = await _unitOfWork.ProductRepository.AddAsync(product);
        if (result is false)
        {
            return Result.Failure<bool>(Error.NotFound("Product", request.CategoryId));
        }
        await _unitOfWork.CommitAsync();

        return Result.Success(result);
    }
}