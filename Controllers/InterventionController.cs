using Examen.DTO.Intervention;
using Examen.Exceptions;
using Examen.Service.Intervention;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    [Route("api/intervention")]
    [ApiController]
    public class InterventionController: ControllerBase
    {
        private readonly IInterventionService _interventionService;

        public InterventionController (IInterventionService interventionService)
        {
            _interventionService = interventionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIntervention (InterventionCreateDto dto)
        {
            await _interventionService.CreateInterventionAsync(dto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterventionById (int id)
        {
            var intervention = await _interventionService.GetInterventionByIdAsync(id);
            if (intervention == null) throw new ElementNotFoundExcetion("notfound.intervention");

            return Ok(intervention);
        }
    }
}
