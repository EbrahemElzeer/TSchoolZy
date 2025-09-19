using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class TeamMemberDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [MaxLength(100, ErrorMessage = "Position must not exceed 100 characters.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Image path is required.")]
        [MaxLength(200, ErrorMessage = "Image path must not exceed 200 characters.")]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "LinkedIn path is required.")]
        [Url(ErrorMessage = "LinkedIn link must be a valid URL starting with http or https.")]
        [RegularExpression(@"^https?://(www\.)?linkedin\.com/.+", ErrorMessage = "LinkedIn link must contain 'linkedin.com'.")]
        public string LinkedInLink { get; set; }
        [JsonIgnore]
        public bool IsUpdate { get; set; }
    }

}
