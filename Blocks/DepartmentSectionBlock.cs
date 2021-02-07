using Piranha.Extend;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiranhaMVC.Blocks
{

    [BlockGroupType(Name = "Servizi", Category = "Sezioni", Icon = "fab fa-suse")]
    [BlockItemType(Type = typeof(DepartmentBlock))]
    public class DepartmentSectionBlock : BlockGroup
    {
        public string NiceName { get; set; } = "Servizi";

        [Field(Title = "Immagine")]
        public ImageField Image { get; set; }

        //public IList<Block> Items { get; set; }
    }

    [BlockType(Name = "Servizio", IsUnlisted =true)]
    public class DepartmentBlock : Block
    {
        [Field(Title = "Icona")]
        public StringField Icon { get; set; }

        [Field(Title = "Titolo")]
        public StringField Title { get; set; }

        [Field(Title = "Collegamento")]
        public StringField Link { get; set; }

        [Field(Title = "Descrizione")]
        public HtmlField Description { get; set; }
    }

}
