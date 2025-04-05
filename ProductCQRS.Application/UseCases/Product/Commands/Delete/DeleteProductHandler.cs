using MapsterMapper;
using MediatR;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{

  
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IMapper _mapper;

    public DeleteProductHandler(IUnitOfWork unitOfWork, IReadUnitOfWork readUnitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _readUnitOfWork = readUnitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        bool result = await _unitOfWork.ProductRepository.DeleteAsync(request.ProductId);
        if (result)
        { 
            return Result.Success(true);
        }
        else
            return Result.Failure<bool>(Error.NotFound("Product" , request.ProductId));
    }
}
