using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class UserStatus : IdentityEntity<byte>
    {
        [StringLength(32)]
        public string Status { get; set; } = null!;
    }
}