using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Application.Dto
{
    public class SettingDto
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Location text is required.")]
        [MaxLength(200, ErrorMessage = "Location text must not exceed 200 characters.")]
        public string LocationText { get; set; }

        [Required(ErrorMessage = "Pin location is required.")]
        public string PinLocation { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Phone number must be valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Facebook link must be a valid URL.")]
        public string FacebookLink { get; set; }

        [Url(ErrorMessage = "Twitter link must be a valid URL.")]
        public string TwitterLink { get; set; }

        [Url(ErrorMessage = "LinkedIn link must be a valid URL.")]
        public string LinkedInLink { get; set; }

        [Url(ErrorMessage = "Instagram link must be a valid URL.")]
        public string InstagramLink { get; set; }

        [Url(ErrorMessage = "YouTube link must be a valid URL.")]
        public string YoutubeLink { get; set; }
        [JsonIgnore]
        public bool IsUpdate { get; set; }
    }

}
