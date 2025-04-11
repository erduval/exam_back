using Examen.Exceptions;
using Examen.Service.TechnicienIntervention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examen.Controllers
{
    [Route("api/technicien")]
    [ApiController]
    [Authorize]
    public class TechnicienController: ControllerBase
    {
        private readonly ITechnicienInterventionService _technicienInterventionService;

        public TechnicienController (ITechnicienInterventionService technicienInterventionService)
        {
            _technicienInterventionService = technicienInterventionService;
        }


        [HttpGet("intervention")]
        [Authorize(Roles = "Technicien")]
        public async Task<IActionResult> GetTechnicienInterventions ()
        {
            var intervention = await _technicienInterventionService.GetIntervention(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (intervention == null) throw new ElementNotFoundExcetion("notfound.technician.intervention");

            return Ok(intervention);
        }
    }
}
