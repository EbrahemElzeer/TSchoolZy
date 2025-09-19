using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class PackageFeature
    {
        public int Id { get; set; }
        public string FeatureText { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }

}
