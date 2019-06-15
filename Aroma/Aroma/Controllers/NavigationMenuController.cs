using Aroma.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace Aroma.Controllers
{
    public class NavigationMenuController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            var currentContextItem = RenderingContext.Current.ContextItem;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var currentRenderingItem = RenderingContext.Current.Rendering.Item;
            if (currentRenderingItem != null)
                homeItem = currentRenderingItem;
            return View(CreateNavigationMenu(homeItem, currentContextItem));
        }

        private NavigationMenu CreateNavigationMenu(Item root, Item current)
        {
            NavigationMenu navMenu = new NavigationMenu
            {
                NavTitle = root.DisplayName,
                NavUrl = LinkManager.GetItemUrl(root),
                Children = root.Axes.IsAncestorOf(current)
                                ?
                                root.GetChildren().Select(i => CreateNavigationMenu(i,current))
                                : null
            };
            return navMenu;
        }
    }
}