using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<PackageFeature> Features { get; set; } = new List<PackageFeature>();

    }
}