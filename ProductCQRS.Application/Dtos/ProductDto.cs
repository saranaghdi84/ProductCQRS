using ProductCQRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Dtos;

public class ProductDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Discount { get; set; }

    public decimal Price { get; set; }

    public decimal PurchasePrice { get; set; }

    public int Rate { get; set; }

    public ColorEnum Color { get; set; }

    public string Description { get; set; }

    public bool IsDeleted { get;  set; }

    public bool IsActivate { get;  set; }

    public int CategoryId { get; set; }
    public ProductCategoryDto Category { get; set; }
}
