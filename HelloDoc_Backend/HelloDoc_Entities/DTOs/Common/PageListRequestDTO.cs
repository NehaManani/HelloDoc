namespace HelloDoc_Entities.DTOs.Common
{
    public class PageListRequestDTO : BaseListRequestDTO
    {
        public string? SearchQuery { get; set; }

        public string? Status { get; set; }
    }
}