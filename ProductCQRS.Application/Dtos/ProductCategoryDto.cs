using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Dtos;

public class ProductCategoryDto
{
    public bool IsDeleted { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }

}
