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
        public class HazardMapController : ControllerBase
        {
            private readonly IHazardMapRepository _hazardMapRepository;

            public HazardMapController(IHazardMapRepository hazardMapRepository)
            {
                _hazardMapRepository = hazardMapRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<HazardMap>>> GetAllHazardMapsAsync()
            {
                var hazardMap = await _hazardMapRepository.GetAllHazardMapsAsync();
                 var hazardMapWithAbsoluteUrls = hazardMap.Select(h => new HazardMap
                {
                    MapID = h.MapID,
                    MapName = h.MapName,
                    ImagePath = GetAbsoluteUrl(h.ImagePath)
                    });

                    return Ok(hazardMapWithAbsoluteUrls);

             return Ok(hazardMapWithAbsoluteUrls);
            }
            private string GetAbsoluteUrl(string relativePath)
            {
                 string scheme = Request.IsHttps ? "https" : "http";
             return $"{scheme}://{Request.Host}{Request.PathBase}/{relativePath.TrimStart('/')}";
            }
        }
    }
