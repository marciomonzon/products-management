namespace ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Base
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T? Data { get; set; }

        public static BaseResponse<T> Ok(T data, string? message = null) => new()
        {
            Success = true,
            Data = data,
            Message = message
        };

        public static BaseResponse<T> Fail(string message) => new()
        {
            Success = false,
            Message = message
        };
    }
}
