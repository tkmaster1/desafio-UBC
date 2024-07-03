namespace UBC.Core.WebApi.Models.Responses
{
    public class ResponseSuccess<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }
    }
}