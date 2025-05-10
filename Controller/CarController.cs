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
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly CarSpecService _carSpecService;
        private readonly AuctionService _auctionService;

        public CarController(CarService carService, CarSpecService carSpecService, AuctionService auctionService)
        {
            _carService = carService;
            _carSpecService = carSpecService;
            _auctionService = auctionService;
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
    }
}
