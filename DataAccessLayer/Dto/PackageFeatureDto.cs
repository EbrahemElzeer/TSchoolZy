using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PackageFeatureDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Feature name is required")]
        [MaxLength(35, ErrorMessage = "Feature name must not exceed 35 characters.")]

        public string FeatureText { get; set; }
      
    }

}
