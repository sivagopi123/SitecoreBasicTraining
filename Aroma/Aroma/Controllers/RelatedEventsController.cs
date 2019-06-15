using Aroma.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace Aroma.Controllers
{
    public class RelatedEventsController : Controller
    {
        // GET: RelatedEvents
        public ActionResult Index()
        {
            var sitecoreItem = RenderingContext.Current.ContextItem;
            MultilistField multiListField = sitecoreItem.Fields["Related Items"];
            var itemArray = multiListField.GetItems();

            var navItemList = itemArray.Select(i => new NavigationItem()
            {
                NavTitle = i.DisplayName,
                NavUrl = LinkManager.GetItemUrl(i)
            });

            return View(navItemList);
        }
    }
}