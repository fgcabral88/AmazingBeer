namespace AmazingBeer.Api.Application.Responses
{
    public class ResponseBase<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResponseBase() { }

        public ResponseBase(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
