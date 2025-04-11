namespace Examen.Models
{
    public class InterventionTechnicien
    {
        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; } = default!;

        public string TechnicienId { get; set; } = string.Empty;
        public AppUser Technicien { get; set; } = default!;
    }
}
