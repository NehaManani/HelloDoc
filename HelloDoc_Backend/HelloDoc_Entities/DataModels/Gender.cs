using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class Gender : IdentityEntity<int>
    {
        [StringLength(20)]
        public string Title { get; set; } = null!;
    }
}