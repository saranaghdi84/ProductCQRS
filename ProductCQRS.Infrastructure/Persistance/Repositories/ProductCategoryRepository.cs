using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Infrastructure.Persistance.Context;

namespace ProductCQRS.Infrastructure.Persistance.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly CommandDbContext _context;

    public ProductCategoryRepository(CommandDbContext context )
    {
        _context = context;
    }

    public async Task<bool> AddAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        
        if (category is null)
        {
            return false;
        }
        var addedCategory = await _context.ProductCategories.AddAsync(category , cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int categoryId)
    {
        var category = _context.ProductCategories.SingleOrDefault(c => c.Id == categoryId);
        if (category is null) { return false; }
        category.Delete();
        return true;
    }

    public async Task<bool> HasProductsAsync(int categoryId)
    {
        return await _context.Products
        .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
        .AnyAsync();
    }

    public async Task<bool> UpdateAsync(ProductCategory category)
    {
      var updatedCategory =  _context.ProductCategories.Update(category) ;
        if(updatedCategory is null) { return false; }
        return true;
    }
}
