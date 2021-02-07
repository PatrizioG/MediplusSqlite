using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PiranhaMVC.Models
{
    public class ContactFormDTO
    {
        [Required(ErrorMessage = "Si prega di inserire il vostro nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Si prega di inserire la vostra email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Si prega di inserire il vostro messaggio")]
        public string Message { get; set; }

        public bool IsPrivacyAccepted { get; set; }
    }
}
