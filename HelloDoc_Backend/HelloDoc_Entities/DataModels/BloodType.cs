using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class BloodType : IdentityEntity<int>
    {
        [StringLength(5)]
        public string BloodGroup { get; set; } = null!;
    }
}