using MediatR;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.ResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.ProductCategory.Queries.GetAllProducts;

public class GetAllProductCategoriesQuery : IRequestHandler<Result<ProductCategoryDto>>
{

}
