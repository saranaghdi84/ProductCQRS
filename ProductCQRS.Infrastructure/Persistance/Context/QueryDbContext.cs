﻿using Microsoft.EntityFrameworkCore;
using ProductCQRS.Domain.Entities;
using System.Reflection;

namespace ProductCQRS.Infrastructure.Persistance.Context;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<ProductCategory>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
