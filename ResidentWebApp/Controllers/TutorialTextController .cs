using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ResidentWebApp.Models;
using ResidentWebApp.Repositories;

namespace YourApp.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class TutorialTextController : ControllerBase
{
    private readonly ITutorialTextRepository _tutorialTextRepository;

    public TutorialTextController(ITutorialTextRepository tutorialTextRepository)
    {
        _tutorialTextRepository = tutorialTextRepository;
    }

    [HttpGet("{disasterName}")]
    public IActionResult GetTutorialText(string disasterName)
    {
        var tutorialText = _tutorialTextRepository.GetTutorialTextByDisasterName(disasterName);
        return tutorialText != null ? Ok(tutorialText) : NotFound();
    }
}
}