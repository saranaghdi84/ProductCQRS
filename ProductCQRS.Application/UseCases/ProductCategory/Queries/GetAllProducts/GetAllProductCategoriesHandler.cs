using MediatR;
using ProductCQRS.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.ProductCategory.Queries.GetAllProducts;

public sealed record  GetAllProductCategoriesHandler : IRequest<IList<ProductCategoryDto>>;