using Piranha.Extend;
using Piranha.Extend.Fields;

namespace PiranhaMVC.Blocks
{
    [BlockType(Name = "Chi Siamo", Category = "Sezioni", Icon = "fab fa-suse")]
    public class AboutSectionBlock : Block
    {
        public string NiceName { get; set; } = "Chi Siamo";

        public StringField Titolo { get; set; }

        public ImageField Immagine { get; set; }

        public HtmlField Descrizione { get; set; }

        [Field(Title = "Numero di telefono")]
        public StringField Number { get; set; }
    }
}
