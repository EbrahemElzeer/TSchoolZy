using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Setting
    {
        public int Id { get; set; }
        public string LocationText { get; set; }
        public string PinLocation { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedInLink { get; set; }
        public string InstagramLink { get; set; }
        public string YoutubeLink { get; set; }
    }

}
