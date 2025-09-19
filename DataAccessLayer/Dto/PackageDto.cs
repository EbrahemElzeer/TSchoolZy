using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PackageDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Icon path is required.")]
        [MaxLength(200, ErrorMessage = "Icon path must not exceed 200 characters.")]
        public string IconPath { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be zero or greater.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Features list cannot be null.")]
        [MinLength(1, ErrorMessage = "Features list must contain at least one feature.")]
        public List<PackageFeatureDto> Features { get; set; }
        [JsonIgnore]
        public bool IsUpdate { get; set; }
    }
}
