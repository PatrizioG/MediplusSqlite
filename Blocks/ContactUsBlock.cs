using Piranha.Extend;
using Piranha.Extend.Fields;
using PiranhaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiranhaMVC.Blocks
{
    [BlockType(Name = "Contattaci", Category = "Sezioni", Icon = "fab fa-suse")]
    public class ContactUsBlock : Block
    {
        public string NiceName { get; set; } = "Contattaci";

        [Field(Title = "Title")]
        public StringField Title { get; set; }

        [Field(Title = "Description")]
        public HtmlField Description { get; set; }

        [Field(Title = "Indirizzo")]
        public HtmlField Address { get; set; }

        [Field(Title = "Telefono")]
        public StringField Number { get; set; }

        [Field(Title = "Email")]
        public StringField Email { get; set; }

        [Field(Title = "WebSite")]
        public StringField Website { get; set; }

        public ContactFormDTO ContactForm { get; set; }
    }
}
