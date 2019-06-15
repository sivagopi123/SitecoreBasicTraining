using System.Collections.Generic;

namespace Aroma.Models
{
    public class NavigationMenu : NavigationItem
    {
        public IEnumerable<NavigationMenu> Children { get; set; }
    }
}