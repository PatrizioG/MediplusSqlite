using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace TryPiranha.Models
{
    [PageType(Title = "Pagina principale", UsePrimaryImage = false, UseExcerpt = false)]
    public class StandardPage  : Page<StandardPage>
    {
    }   
}