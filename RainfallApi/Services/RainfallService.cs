using RainfallApi.Models;
using System.Text.Json;

namespace RainfallApi.Services
{
    public class RainfallService
    {
        private readonly HttpClient _httpClient;

        // I know it's better to take it out, but I do it in a hurry
        private readonly string path = "https://environment.data.gov.uk/flood-monitoring";

        public RainfallService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<RainfallReadingModel>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            var response = await _httpClient.GetAsync($"{path}/id/stations/{stationId}/readings?_sorted&_limit={count}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch rainfall readings. Status code: {response.StatusCode}");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var rainfallApiResponse = await response.Content.ReadFromJsonAsync<RainfallApiResponseModel>();

            if (rainfallApiResponse == null) throw new ArgumentNullException(nameof(rainfallApiResponse));

            var rainfallReadings = rainfallApiResponse.Items.Select(item => new RainfallReadingModel
            {
                DateMeasured = item.dateTime,
                AmountMeasured = item.value
            }).ToList();

            return rainfallReadings.ToList();
        }
    }
}
