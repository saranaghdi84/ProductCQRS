﻿using MediatR;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Queries.GetProductsById;

public record GetProductsByIdQuery(int Id): IRequest<Result<ProductDto>>;
