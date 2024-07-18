using System.Net;
using HelloDoc_Common.Constants;
using HelloDoc_Entities.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_DataAccessLayer.Helpers
{
    public class ResponseHelper
    {

        public static IActionResult CreatedResponse<T>(T? data, string? message, bool success)
        {
            HttpStatusCode statusCode = success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            ApiResponse<T> result = new()
            {
                StatusCode = (int)statusCode,
                Message = message ?? string.Empty,
                Data = data,
                Success = success,
            };
            return new ObjectResult(result) { StatusCode = (int)statusCode };
        }

        public static IActionResult CreatePageResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalPage, long totalRecords = 0)
        {
            PageResponse<T> result = new(data, pageNumber, pageSize, totalPage, totalRecords);
            ApiResponse<PageResponse<T>> response = new()
            {
                Success = true,
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
            };
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}