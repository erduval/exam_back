using Examen.Data;
using Examen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen.DataAccess
{
    public class ServiceTypeDataAccess: IServiceTypeDataAccess
    {
        private readonly AppDbContexte _context;

        public ServiceTypeDataAccess (AppDbContexte context)
        {
            _context = context;
        }

        public async Task<List<ServiceType>> GetAllServiceTypeAsync ()
        {
            return await _context.ServiceTypes.ToListAsync();
        }

        public async Task<ServiceType> GetServiceTypeByIdAsync (int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                throw new InvalidOperationException($"ServiceType with ID {id} not found.");
            }
            return serviceType;
        }

        public async Task CreateServiceTypeAsync (ServiceType serviceType)
        {
            _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();
        }
    }
}
