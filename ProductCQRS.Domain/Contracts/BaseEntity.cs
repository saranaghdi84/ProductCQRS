#nullable disable
using ProductCQRS;

namespace ProductCQRS.Domain.Contracts;

public class BaseEntity<T>
{
    public T Id { get; set; }

    public string Name { get; set; }

    public DateTime CreateDateTime { get; set; }
}
