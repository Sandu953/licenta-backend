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
    public class CarUploadDto
    {
        // Car info
        public string Title { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Fuel { get; set; }
        public string Trim { get; set; }
        public string TrimId { get; set; }
        public string Mileage { get; set; }
        public string Horsepower { get; set; }
        public string BodyType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // Auction info
        public string Price { get; set; }
        public string ReservePrice { get; set; }

        // Images
        public List<IFormFile> Images { get; set; }
    }

    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly CarSpecService _carSpecService;
        private readonly AuctionService _auctionService;
        private readonly CarImageService _carImageService;

        public CarController(CarService carService, CarSpecService carSpecService, AuctionService auctionService, CarImageService carImageService)
        {
            _carService = carService;
            _carSpecService = carSpecService;
            _auctionService = auctionService;
            _carImageService = carImageService;
        }

        [Authorize]
        [HttpGet("getAllBrands")]
        public IActionResult GetAllCars()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            var carBrands = _carSpecService.GetAllBrands();
            return Ok(carBrands);
        }

        [Authorize]
        [HttpGet("getAllModels/{brand}")]
        public IActionResult GetAllModels(string brand)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            var carModels = _carSpecService.GetAllModelsByBrand(brand);
            return Ok(carModels);
        }

        [Authorize]
        [HttpGet("getAllEngineSize/{brand}/{model}")]
        public IActionResult GetAllEngineSize(string brand, string model)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            var engineSizes = _carSpecService.GetAllEngineSizesByBrandModel(brand, model);
            return Ok(engineSizes);
        }

        [Authorize]
        [HttpGet("getAllFuelType/{brand}/{model}/{engineSize}")]
        public IActionResult GetAllFuelTypes(string brand, string model, int engineSize)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            var fuelTypes = _carSpecService.GetAllFuelTypeByBrandModelEngineSize(brand, model, engineSize);
            return Ok(fuelTypes);
        }

        [Authorize]
        [HttpGet("getAllTrims/{brand}/{model}/{engineSize}/{fuelType}")]
        public IActionResult GetAllTrims(string brand, string model, int engineSize, string fuelType)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            var trims = _carSpecService.GetAllTrimsByBrandModelEngineSizeFuelType(brand, model, engineSize, fuelType);
            return Ok(trims);
        }

        [Authorize]
        [HttpGet("verifyVinInLiveAuction/{vin}")]
        public IActionResult VerifyVinInLiveAuction(string vin)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            try
            {
                var isVinInLiveAuction = _auctionService.FindLiveAuctionByCarVin(vin);
                if (isVinInLiveAuction == null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Car is already in a live auction");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCar([FromForm] CarUploadDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null) return Unauthorized();

            try
            {
                var userId = int.Parse(userIdClaim);

                var car = _carService.Save(int.Parse(dto.TrimId), dto.Vin, int.Parse(dto.Mileage),int.Parse(dto.Year),dto.BodyType, dto.Location, dto.Description);

                var auction = _auctionService.Save(car.Id, userId, dto.Title, DateTime.UtcNow, int.Parse(dto.Price), int.Parse(dto.ReservePrice));

                var carFolder = Path.Combine("wwwroot/uploads", car.Id.ToString());
                Directory.CreateDirectory(carFolder);

                if (dto.Images == null || !dto.Images.Any())
                {
                    return BadRequest(new { error = "Images were not received." });
                }

                foreach (var image in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var filePath = Path.Combine(carFolder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    _carImageService.Save(car.Id, $"/uploads/{car.Id}/{fileName}");
                }

                return Ok(new { carId = car.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new { message = e.Message });
                
            }
        }
    }
}
