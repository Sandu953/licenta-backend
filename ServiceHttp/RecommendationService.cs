using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceHttp
{
    public class RecommendationService
    {
        private readonly HttpClient _httpClient;

        public RecommendationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CarRecommendation>> GetRecommendationsAsync(int userId)
        {
            var requestBody = new { user_id = userId };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:8000/recommend", requestBody);

            response.EnsureSuccessStatusCode();

            var recommendResponse = await response.Content.ReadFromJsonAsync<RecommendResponse>();

            return recommendResponse.Recommendations;
        }
    }

    public class RecommendResponse
    {
        public List<CarRecommendation> Recommendations { get; set; }
    }

    public class CarRecommendation
    {
        public string Maker { get; set; }
        public string Genmodel { get; set; }
        public string ItemId { get; set; }
        public float Score { get; set; }
    }
}
