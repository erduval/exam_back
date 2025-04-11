using Examen.Data;
using Examen.DTO.Intervention;
using Examen.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen.Service.Intervention
{
    public class InterventionService: IInterventionService
    {
        private readonly AppDbContexte _context;

        public InterventionService (AppDbContexte context)
        {
            _context = context;
        }

        public async Task CreateInterventionAsync (InterventionCreateDto dto)
        {
            if (dto.ScheduledAt < DateTime.Now)
            {
                throw new ArgumentException("La date indiquée ne peut pas être avant aujourd'hui");
            }

            var intervention = new Models.Intervention
            {
                ClientId = dto.ClientId,
                ServiceTypeId = dto.ServiceTypeId,
                ScheduledAt = dto.ScheduledAt,
                Description = dto.Description
            };

            var techniciens = await _context.Users
                .Where(u => dto.TechnicienIds.Contains(u.Id))
                .ToListAsync();

            foreach (var technicien in techniciens)
            {
                intervention.TechnicienLinks.Add(new InterventionTechnicien
                {
                    Intervention = intervention,
                    Technicien = technicien
                });
            }

            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();
        }

        public async Task<InterventionReadDto> GetInterventionByIdAsync (int id)
        {
            var intervention = await _context.Interventions
                .Include(i => i.Client)
                .Include(i => i.ServiceType)
                .Include(i => i.TechnicienLinks)
                .ThenInclude(it => it.Technicien)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (intervention == null) return null;

            var interventionReadDto = new InterventionReadDto
            {
                Id = intervention.Id,
                ClientId = intervention.ClientId,
                ServiceTypeId = intervention.ServiceTypeId,
                ScheduledAt = intervention.ScheduledAt,
                Description = intervention.Description,
                ClientFullName = intervention.Client.FullName,
                ServiceTypeName = intervention.ServiceType.Name,
                TechnicienIds = intervention.TechnicienLinks.Select(it => it.TechnicienId).ToList()
            };

            return interventionReadDto;
        }
    }
}
