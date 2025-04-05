using Mapster;
using ProductCQRS.Application.Dtos;
using ProductCQRS.Application.UseCases.Product.Commands.Add;
using ProductCQRS.Application.UseCases.Product.Commands.Update;
using ProductCQRS.Domain.Entities;
namespace ProductCQRS.Application.UseCases.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductCQRS.Domain.Entities.Product, ProductDto>()
            .Map(dest => dest.Category, src => src.Category.Adapt<ProductCategoryDto>());

        config.NewConfig<ProductDto, ProductCQRS.Domain.Entities.Product>();
        config.NewConfig<ProductCQRS.Domain.Entities.ProductCategory, ProductCategoryDto>();
        config.NewConfig<ProductCategoryDto, ProductCQRS.Domain.Entities.ProductCategory>();
        config.NewConfig<AddProductCommand, ProductCQRS.Domain.Entities.Product>();
        config.NewConfig<UpdateProductCommand, ProductCQRS.Domain.Entities.Product>();
    }
}
