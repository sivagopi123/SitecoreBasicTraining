using Aroma.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace Aroma.Controllers
{
    public class BreadCrumbController : Controller
    {
        // GET: BreadCrumb
        public ActionResult Index()
        {

            var currentContextItem = RenderingContext.Current.ContextItem;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var breadCrumbObj = RenderingContext.Current.ContextItem.Axes.GetAncestors()
                                .Where(i => i.Axes.IsDescendantOf(homeItem))
                                .Concat(new Item[] { currentContextItem })
                                .ToList();

            var navItemList = breadCrumbObj.Select(item => new NavigationItem
            {
                NavTitle = item.DisplayName,
                NavUrl = LinkManager.GetItemUrl(item),
                ActiveFlagString = (item.ID == currentContextItem.ID) ? "active" : ""
            });

            return View(navItemList);
        }
    }
}