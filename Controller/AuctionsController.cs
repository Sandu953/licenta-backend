using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controller
{
    public class AuctionFilterDto
    {
        public string Status { get; set; } = "active";
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public int? HpMin { get; set; }
        public int? HpMax { get; set; }
        public int? EngineSizeMin { get; set; }
        public int? EngineSizeMax { get; set; }
        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public int? MileageMin { get; set; }
        public int? MileageMax { get; set; }
        public string? Fuel { get; set; }
        public string? BodyType { get; set; }
        public int? Page { get; set; }
        public int? Cursor { get; set; }
        public int PageSize { get; set; } = 10;
        public int? userId { get; set; } = null; // for favorites
    }

    public class AuctionPreviewSendDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Location { get; set; }
        public int? Year { get; set; }
        public int? Mileage { get; set; }
        public int? CurrentBid { get; set; }
        public DateTime? StartTime { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFavorite { get; set; } = false;
        public string? OwnerUserName { get; set; }
        public string? OwnerPfp { get; set; }
    }

    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionService _auctionService;
        private readonly CarService _carService;
        private readonly CarImageService _carImageService;

        public AuctionsController(AuctionService auctionService, CarService carService, CarImageService carImageService)
        {
            _auctionService = auctionService;
            _carService = carService;
            _carImageService = carImageService;
        }

        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] AuctionFilterDto filter)
        {
            try
            {
                var auctions = _auctionService.Filter(filter);
                return Ok(auctions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
