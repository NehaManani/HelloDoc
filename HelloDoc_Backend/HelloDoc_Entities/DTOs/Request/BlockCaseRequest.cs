using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.DTOs.Request
{
    public class BlockCaseRequest
    {
        [Required]
        public int UserId { get; set; }

        public string? ReasonForBlock { get; set; }
    }
}