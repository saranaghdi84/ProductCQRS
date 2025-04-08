using MediatR;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.ProductCategory.Commands.Add;

public record AddProductCategoryCommand(long ProductId, string Name,bool IsDeleted) : IRequest<Result<bool>>;

