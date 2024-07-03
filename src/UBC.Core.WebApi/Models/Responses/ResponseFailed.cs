namespace UBC.Core.WebApi.Models.Responses
{
    public class ResponseFailed
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}