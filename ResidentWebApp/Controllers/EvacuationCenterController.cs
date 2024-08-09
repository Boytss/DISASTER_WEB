    using Microsoft.AspNetCore.Mvc;
    using ResidentWebApp.Repositories;
    using System.Threading.Tasks;
    using ResidentWebApp.Data;
    using ResidentWebApp.Models;
    using Microsoft.AspNetCore.Authorization;


    namespace ResidentWebApp.Controllers
    {
        [Route("api/[controller]")]
        
        [ApiController]
        public class EvacuationCenterController : ControllerBase
        {
            private readonly IEvacuationCenterRepository _repository;
            private readonly IWebHostEnvironment _env;

            public EvacuationCenterController(IEvacuationCenterRepository repository, IWebHostEnvironment env)
            {
                _repository = repository;
                _env = env;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                try
                {
                    var centers = await _repository.GetAllAsync();
                    if (!centers.Any())
                        return NoContent(); // 204 No Content if the list is empty
                    return Ok(centers);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error in GetAll: {ex.Message}");
                    Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                        Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");

                    // More detailed error in development
                    if (_env.IsDevelopment())
                        return StatusCode(500, $"Error: {ex.Message}. Inner: {ex.InnerException?.Message}");
                    else
                        return StatusCode(500, "An error occurred while fetching evacuation centers.");
                }
            }
            [HttpGet("latest")]
            public async Task<IActionResult> GetLatest()
            {
                try
                {
                    var latestCenter = await _repository.GetLatestCenterAsync();
                    if (latestCenter == null)
                        return NotFound(); // 404 Not Found if no center is available

                    return Ok(latestCenter);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error in GetLatest: {ex.Message}");
                    Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                        Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");

                    // More detailed error in development
                    if (_env.IsDevelopment())
                        return StatusCode(500, $"Error: {ex.Message}. Inner: {ex.InnerException?.Message}");
                    else
                        return StatusCode(500, "An error occurred while fetching the latest evacuation center.");
                }
            }
            [HttpGet("{centerId}/rooms")]
        public async Task<IActionResult> GetRoomsForCenter(int centerId)
        {
            try
                {
                    var center = await _repository.GetCenterByIdAsync(centerId);
                    if (center == null)
                        return NotFound(new { message = $"Evacuation Center with ID {centerId} not found." });

                    var rooms = await _repository.GetRoomsForEvacuationCenterAsync(centerId);
                    var result = new
                    {
                        centerName = center.CenterName,
                        location = center.Location,
                        rooms = rooms.Select(r => new
                        {
                            roomNumber = r.RoomNumber,
                            capacity = r.Capacity
                        })
                    };

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error in GetRoomsForCenter: {ex.Message}");
                    return StatusCode(500, new { message = "An error occurred while fetching evacuation rooms." });
                }
        }
            
      }
    }
