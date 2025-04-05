using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.Contracts;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Infrastructure.Persistance.Context;

namespace ProductCQRS.Infrastructure.Persistance.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CommandDbContext _context;

    public ProductRepository(CommandDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Activate(int productId)
    {
        var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        if (product is null) { return false; }
        product.Activate();
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> AddAsync(Product product)
    {
        if (product is null)
        {
            return false;
        }
        var addedProduct = await _context.Products.AddAsync(product);
        return true;
    }

    public async Task<bool> DeleteAsync(long ID)
    {
        var product = _context.Products.SingleOrDefault(c => c.Id == ID);
        if (product is null) { return false; }
        product.Delete();
        return true;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var updatedProduct = _context.Products.Update(product);
        if (updatedProduct is null) { return false; }
        return true;
    }
}
