using Aroma.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System.Web;
using System.Web.Mvc;

namespace Aroma.Controllers
{
    public class ItemDetailsController : Controller
    {
        // GET: ItemDetails
        public ActionResult Index()
        {
            var sitecoreItem = RenderingContext.Current.ContextItem;
            ItemDetails itemDetails = new ItemDetails()
            {
                CakeTitle = new HtmlString(FieldRenderer.Render(sitecoreItem, "Cake Title")),
                CakeImage = new HtmlString(FieldRenderer.Render(sitecoreItem, "Cake Image","mw=400")),
                CakeDescription = new HtmlString(FieldRenderer.Render(sitecoreItem, "Cake Description"))
            };

            return View(itemDetails);
        }
    }
}