using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ResidentWebApp.Models;
using ResidentWebApp.Repositories;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;

        public VideosController(IVideoRepository videoRepository)
            {
                _videoRepository = videoRepository;
            }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Video>>> GetAllVideosAsync()
        {
            var videos = await _videoRepository.GetAllVideosAsync();
            return Ok(videos);
        }
    }
}