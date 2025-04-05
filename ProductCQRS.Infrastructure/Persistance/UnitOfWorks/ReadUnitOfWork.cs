using ProductCQRS.Application.Contracts;
using ProductCQRS.Infrastructure.Persistance.Context;
using ProductCQRS.Infrastructure.Persistance.Repositories;

namespace ProductCQRS.Infrastructure.Persistance.UnitOfWorks;

public class ReadUnitOfWork : IReadUnitOfWork
{
    private readonly QueryDbContext _context;
    private IProductReadRepository _productRepository;
    private IProductCategoryReadRepository _categoryRepository;

    public ReadUnitOfWork(QueryDbContext context) => _context = context;

    public IProductReadRepository ProductReadRepository =>
        _productRepository ??= new ProductReadRepository(_context);

    public IProductCategoryReadRepository ProductCategoryReadRepository =>
        _categoryRepository ??= new ProductCategoryReadRepository(_context);

    public void Dispose() => _context.Dispose();

}
