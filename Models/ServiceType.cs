namespace Examen.Models
{
    public class ServiceType
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;

        public ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
