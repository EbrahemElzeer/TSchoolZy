using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Interface
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        Task<Package> GetByIdWithFeaturesAsync(int id);
        Task<IEnumerable<Package>> GetAllWithFeaturesAsync();
    }

}
