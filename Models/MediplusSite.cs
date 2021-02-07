using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace PiranhaMVC.Models
{
    [SiteType(Title = "Mediplus Site")]
    public class MediplusSite : SiteContent<MediplusSite>
    {
        [Region(Display = RegionDisplayMode.Setting)]
        public Hero Hero { get; set; }
    }

    public class Hero
    {
        [Field(Title = "Immagine")]
        public ImageField Image { get; set; }

        [Field(Title = "Titolo")]
        public HtmlField Title { get; set; }

        [Field(Title = "Sottotitolo")]
        public StringField Subtitle { get; set; }

        [Field(Title = "Descrizione")]
        public HtmlField Description { get; set; }

        [Field(Title = "Abilita pulsante chiamaci")]
        public CheckBoxField EnableCallUs { get; set; }

        [Field(Title = "Numero di telefono")]
        public StringField Number { get; set; }

    }
}
