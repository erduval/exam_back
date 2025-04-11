using Examen.DTO.Intervention;

namespace Examen.Service.TechnicienIntervention
{
    public interface ITechnicienInterventionService
    {
        Task<List<InterventionReadDto>> GetIntervention (string idTechnicien);
    }
}
