using Examen.DTO.ServiceType;

namespace Examen.Service.ServiceType
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeReadDto>> GetAllServiceTypesAsync ();
        Task<ServiceTypeReadDto> GetServiceTypeByIdAsync (int id);
        Task CreateServiceTypeAsync (ServiceTypeCreateDto serviceTypeCreateDto);
    }
}
