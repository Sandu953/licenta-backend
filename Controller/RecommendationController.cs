using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.ServiceHttp;

[ApiController]
[Route("[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly RecommendationService _recommendationService;

    public RecommendationController(RecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetRecommendations(int userId)
    {
        var recommendations = await _recommendationService.GetRecommendationsAsync(userId);
        Console.WriteLine("Recommendations: " + recommendations.Count);
        return Ok(recommendations);
    }
}