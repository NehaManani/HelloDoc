using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class UserRole : IdentityEntity<byte>
    {
        [StringLength(16)]
        public string Role { get; set; } = null!;
    }
}