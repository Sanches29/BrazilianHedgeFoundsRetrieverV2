namespace BrazilianHedgeFoundsRetriever.Responses
{
    public class ApiResponse
    {
        public ApiResponse(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        public bool Success { get; set; }
        public object Data { get; set; }
    }
}
