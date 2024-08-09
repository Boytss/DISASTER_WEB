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
        public class NewsEventsController : ControllerBase
        {
            private readonly INewsEventRepository _newsEventRepository;

            public NewsEventsController(INewsEventRepository newsEventRepository)
            {
                _newsEventRepository = newsEventRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<NewsEvents>>> GetNewsEvents()
            {
                var newsEvents = await _newsEventRepository.GetAllNewsEventsAsync();
                 var newsEventsWithAbsoluteUrls = newsEvents.Select(ne => new NewsEvents
                {
                    EventID = ne.EventID,
                    Title = ne.Title,
                    Date = ne.Date,
                    Description = ne.Description,
                    By = ne.By,
                    // Combine the base URL with the relative path
                    ImagePath = GetAbsoluteUrl(ne.ImagePath)
                    });

                    return Ok(newsEventsWithAbsoluteUrls);

             return Ok(newsEventsWithAbsoluteUrls);
            }
            private string GetAbsoluteUrl(string relativePath)
            {
                 string scheme = Request.IsHttps ? "https" : "http";
             return $"{scheme}://{Request.Host}{Request.PathBase}/{relativePath.TrimStart('/')}";
            }
        }
    }
