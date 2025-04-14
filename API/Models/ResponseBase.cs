namespace API.Models
{
    public class ResponseBase<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Response { get; set; }
    }
}
