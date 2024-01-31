using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;
using RainfallApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly RainfallService _rainfallService;

        public RainfallController(RainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("/rainfall/id/{stationId}/readings")]
        [SwaggerOperation(
            Summary = "Get rainfall readings by station Id",
            Description = "Retrieve the latest readings for the specified stationId",
            OperationId = "get-rainfall"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No readings found for the specified stationId", typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ErrorModel))]
        public async Task<ActionResult<RainfallReadingResponseModel>> GetRainfallReadings(string stationId, [FromQuery] int count = 10)
        {
            try
            {
                var readings = await _rainfallService.GetRainfallReadingsAsync(stationId, count);

                if (readings == null || readings.Count == 0)
                {
                    return NotFound();
                }

                var responseModel = new RainfallReadingResponseModel
                {
                    Readings = readings
                };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorModel
                {
                    Message = "Internal server error",
                    Detail = new List<ErrorDetailModel> { new ErrorDetailModel { PropertyName = "Exception", Message = ex.Message } }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }   
}