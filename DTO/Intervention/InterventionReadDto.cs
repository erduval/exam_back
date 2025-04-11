namespace Examen.DTO.Intervention
{
    public class InterventionReadDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public List<string> TechnicienIds { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Description { get; set; }
    }
}
