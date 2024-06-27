using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.Abstract;
public abstract class IdentityEntity<T>
{
    [Key]
    public T Id { get; set; }
}
