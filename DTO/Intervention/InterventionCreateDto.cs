namespace Examen.DTO.Intervention
{
    public class InterventionCreateDto
    {
        public string ClientId { get; set; }
        public int ServiceTypeId { get; set; }
        public List<string> TechnicienIds { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Description { get; set; }
    }
}
