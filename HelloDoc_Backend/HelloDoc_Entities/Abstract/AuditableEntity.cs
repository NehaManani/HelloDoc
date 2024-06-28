
using System.ComponentModel.DataAnnotations.Schema;
using HelloDoc_Entities.DataModels;

namespace HelloDoc_Entities.Abstract;
public abstract class AuditableEntity<T> : IdentityEntity<T>
{
    public virtual DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

    public virtual DateTimeOffset? UpdatedOn { get; set; }

    public virtual DateTimeOffset? DeletedOn { get; set; }
}
