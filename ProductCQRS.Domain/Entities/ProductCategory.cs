using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCQRS.Domain.Contracts;

namespace ProductCQRS.Domain.Entities;

public class ProductCategory : BaseEntity<int>
{
    #region Properties

    public bool IsDeleted { get; private set; }
    public void Delete() => IsDeleted = true;

    #endregion Properties

    #region Relations

    public long ProductId { get; set; }

    public ICollection<Product> Products { get; set; }

    #endregion Relations
}

