using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ClientDto
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image path is required.")]
        [MaxLength(200, ErrorMessage = "Image path must not exceed 200 characters.")]
        public string ImagePath { get; set; }
        [JsonIgnore]
        public bool IsUpdate { get; set; }
    }

}
