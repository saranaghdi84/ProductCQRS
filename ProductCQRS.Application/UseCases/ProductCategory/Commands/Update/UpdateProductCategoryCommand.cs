using MediatR;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.ProductCategory.Commands.Update;

public record UpdateProductCategoryCommand(
 int Id,
 string Name,
 bool IsDeleted,
 int ProductId
 ) : IRequest<Result<bool>>;
