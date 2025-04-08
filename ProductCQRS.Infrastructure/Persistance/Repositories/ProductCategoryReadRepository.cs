using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Infrastructure.Persistance.Context;

namespace ProductCQRS.Infrastructure.Persistance.Repositories;

public class ProductCategoryReadRepository : IProductCategoryReadRepository
{
    private readonly QueryDbContext _context;
    public ProductCategoryReadRepository(QueryDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductCategories.AsNoTracking().ToListAsync(); 
    }


    public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories.FindAsync(id);
    }
}
