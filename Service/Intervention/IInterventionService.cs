using Examen.DTO.Intervention;

namespace Examen.Service.Intervention
{
    public interface IInterventionService
    {
        Task<InterventionReadDto> GetInterventionByIdAsync (int id);
        Task CreateInterventionAsync (InterventionCreateDto interventionCreateDto);
    }
}
