using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interface;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Repositey.Data;
using Repositey.Migrations;

namespace Repositey.GenericRepository
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Package> GetByIdWithFeaturesAsync(int id)
        {
            return await _context.Packages
                .Include(p => p.Features)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Package>> GetAllWithFeaturesAsync()
        {
            return await _context.Packages
                .Include(p => p.Features)
                .ToListAsync();
        }
    }


}
