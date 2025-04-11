namespace Examen.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Description { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;
        public AppUser Client { get; set; } = default!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = default!;

        public ICollection<InterventionTechnicien> TechnicienLinks { get; set; } = new List<InterventionTechnicien>();
    }
}
