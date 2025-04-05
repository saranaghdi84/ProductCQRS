using MediatR;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Commands.Delete;

public sealed record DeleteProductCommand(long ProductId) : IRequest<Result<bool>>;
