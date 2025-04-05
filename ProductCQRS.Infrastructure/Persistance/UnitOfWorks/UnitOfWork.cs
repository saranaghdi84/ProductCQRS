using ProductCQRS.Application.Contracts;
using ProductCQRS.Infrastructure.Persistance.Context;
using ProductCQRS.Infrastructure.Persistance.Repositories;

namespace ProductCQRS.Infrastructure.Persistance.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly CommandDbContext _context;
    private IProductRepository _productRepository;
    private IProductCategoryRepository _categoryRepository;

    public UnitOfWork(CommandDbContext context) => _context = context;

    public IProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(_context);

    public IProductCategoryRepository ProductCategoryRepository =>
        _categoryRepository ??= new ProductCategoryRepository(_context);

    public async Task CommitAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();

}
