using Examen.DTO.ServiceType;
using Examen.Exceptions;
using Examen.Service.ServiceType;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examen.Controllers
{
    [Route("api/service-type")]
    [ApiController]
    public class ServiceTypeController: ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController (IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get (int? id = null)
        {
            if (id != null)
            {
                var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync((int)id);
                if (serviceType == null)
                {
                    throw new ElementNotFoundExcetion("notfound.servicetype");
                }
                return Ok(serviceType);
            }

            var articles = await _serviceTypeService.GetAllServiceTypesAsync();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] ServiceTypeCreateDto serviceTypeCreateDto)
        {
            await _serviceTypeService.CreateServiceTypeAsync(serviceTypeCreateDto);
            return Ok();
        }
    }
}
