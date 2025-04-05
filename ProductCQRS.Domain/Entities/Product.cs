using ProductCQRS.Domain.Contracts;
using ProductCQRS.Domain.Enums;

#nullable enable

namespace ProductCQRS.Domain.Entities;

public class Product : BaseEntity<long>
{
    #region Properties

    public decimal Discount { get; set; }

    public decimal Price { get; set; }

    public decimal PurchasePrice { get; set; }

    public int Rate { get; set; }

    public ColorEnum Color { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; private set; }

    public bool IsActivate { get; private set; }

    public void Activate() => IsActivate = true;

    public void Disactive() => IsActivate = false;

    public void Delete() => IsDeleted = true;


    #endregion Properties

    #region Relations

    public int CategoryId { get; set; }
    public ProductCategory Category { get; set; }

    #endregion Relations
}
