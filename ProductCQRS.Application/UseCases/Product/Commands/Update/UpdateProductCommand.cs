using MediatR;
using ProductCQRS.Application.ResultHandler;
using ProductCQRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Commands.Update;

public record UpdateProductCommand(

 long Id,

 string Name,

 decimal Discount,

 decimal Price,

 decimal PurchasePrice,

 int Rate,

 ColorEnum Color,

 string Description,

 bool IsDeleted,

 bool IsActivate,

int CategoryId) : IRequest<Result<bool>>;

