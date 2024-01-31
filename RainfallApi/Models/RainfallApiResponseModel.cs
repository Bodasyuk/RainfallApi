using System.Text.Json.Serialization;

namespace RainfallApi.Models
{
    public class RainfallApiResponseModel
    {
        [JsonPropertyName("@context")]
        public string Context { get; set; }

        public Meta Meta { get; set; }

        public List<RainfallReadingItem> Items { get; set; }
    }

    public class Meta
    {
        public string Publisher { get; set; }
        public string Licence { get; set; }
        public string Documentation { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }
        public List<string> HasFormat { get; set; }
        public int Limit { get; set; }
    }

    public class RainfallReadingItem
    {
        [JsonPropertyName("@id")]
        public string Id { get; set; }

        public DateTime dateTime { get; set; }

        public string measure { get; set; }

        public decimal value { get; set; }
    }

}
