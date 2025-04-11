using Examen.Data;
using Examen.DataAccess;
using Examen.DTO.ServiceType;
using Examen.Models;

namespace Examen.Service.ServiceType
{
    public class ServiceTypeService: IServiceTypeService
    {
        private readonly IServiceTypeDataAccess _serviceTypeDataAccess;

        public ServiceTypeService (IServiceTypeDataAccess serviceTypeDataAccess)
        {
            _serviceTypeDataAccess = serviceTypeDataAccess;
        }

        public async Task<List<ServiceTypeReadDto>> GetAllServiceTypesAsync ()
        {
            var serviceTypes = await _serviceTypeDataAccess.GetAllServiceTypeAsync();
            return serviceTypes.Select(st => new ServiceTypeReadDto
            {
                Id = st.Id,
                Name = st.Name,
            }).ToList();
        }

        public async Task<ServiceTypeReadDto> GetServiceTypeByIdAsync (int id)
        {
            var serviceType = await _serviceTypeDataAccess.GetServiceTypeByIdAsync(id);
            return new ServiceTypeReadDto
            {
                Id = serviceType.Id,
                Name = serviceType.Name,
            };
        }

        public async Task CreateServiceTypeAsync (ServiceTypeCreateDto serviceTypeCreateDto)
        {
            var serviceType = new Models.ServiceType
            {
                Name = serviceTypeCreateDto.Name,
            };
            await _serviceTypeDataAccess.CreateServiceTypeAsync(serviceType);
        }
    }
}
