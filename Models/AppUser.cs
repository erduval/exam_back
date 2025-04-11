using Microsoft.AspNetCore.Identity;

namespace Examen.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Intervention> ClientInterventions { get; set; }
        public ICollection<InterventionTechnicien> TechnicienInterventions { get; set; }
    }
}
