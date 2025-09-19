using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ContactFormDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(200, ErrorMessage = "Subject must not exceed 200 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public bool IsUpdate { get; set; }

    }

}
