using Examen.Data;
using Examen.DTO.Intervention;
using Microsoft.EntityFrameworkCore;

namespace Examen.Service.TechnicienIntervention
{
    public class TechnicienInterventionService : ITechnicienInterventionService
    {
        private readonly AppDbContexte _context;

        public TechnicienInterventionService (AppDbContexte context)
        {
            _context = context;
        }

        public async Task<List<InterventionReadDto>> GetIntervention (string technicienId)
        {
            var test = technicienId;
            return await _context.Interventions
                .Where(i => i.TechnicienLinks.Any(link => link.TechnicienId == technicienId))
                .Select(i => new InterventionReadDto
                {
                    Id = i.Id,
                    Description = i.Description,
                    ScheduledAt = i.ScheduledAt,
                    ServiceTypeName = i.ServiceType.Name,
                    ClientFullName = i.Client.FullName,
                    TechnicienIds = i.TechnicienLinks.Select(t => t.Technicien.FullName).ToList()
                })
                .ToListAsync();
        }
    }
}
