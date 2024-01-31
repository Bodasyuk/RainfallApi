namespace RainfallApi.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public List<ErrorDetailModel> Detail { get; set; }
    }
}
