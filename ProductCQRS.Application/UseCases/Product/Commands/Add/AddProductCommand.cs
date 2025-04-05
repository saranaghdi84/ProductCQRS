using MediatR;
using Microsoft.AspNetCore.Http.Features;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Domain.Enums;

namespace ProductCQRS.Application.UseCases.Product.Commands.Add;

public record AddProductCommand( string Name,

decimal Discount ,

 decimal Price,

 decimal PurchasePrice ,

 int Rate ,

 ColorEnum Color ,

 string Description ,

 bool IsDeleted ,

 bool IsActivate ,

int CategoryId 
): IRequest<Result<bool>>;
