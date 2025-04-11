using Examen.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examen.DataAccess
{
    public interface IServiceTypeDataAccess
    {
        Task<List<ServiceType>> GetAllServiceTypeAsync ();
        Task<ServiceType> GetServiceTypeByIdAsync (int id);
        Task CreateServiceTypeAsync (ServiceType serviceType);
    }
}
