using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Infrastructure.Persistance.Context;

namespace ProductCQRS.Infrastructure.Persistance.Repositories;

public class ProductReadRepository : IProductReadRepository
{
    private readonly QueryDbContext _context;

    public ProductReadRepository(QueryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        return await _context.Products.FindAsync(id);
    }
}
