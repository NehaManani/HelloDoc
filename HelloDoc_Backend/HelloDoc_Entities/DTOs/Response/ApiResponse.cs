
namespace HelloDoc_Entities.DTOs.Response
{
    public class ApiResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}