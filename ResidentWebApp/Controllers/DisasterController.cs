using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ResidentWebApp.Models;
using ResidentWebApp.Repositories;

namespace YourApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisastersController : ControllerBase
    {
        private readonly IDisasterRepository _disasterRepository;

        public DisastersController(IDisasterRepository disasterRepository)
        {
            _disasterRepository = disasterRepository;
        }

        [HttpGet]
        public IActionResult GetDisasters()
        {
            var disasters = _disasterRepository.GetAllDisasters();
            return Ok(disasters);
        }
    }
}