using Aroma.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System.Web.Mvc;

namespace Aroma.Controllers
{
    public class CommentFormController : Controller
    {
        // GET: CommentForm
        public ActionResult Index()
        {
            CommentForm formObj = new CommentForm
            {
                UserName = "",
                UserComment = ""
            };
            return View(formObj);
        }
        [HttpPost]
        public ActionResult Index(string UserName, string UserComment)
        {

            var contextItem = RenderingContext.Current.ContextItem;
            var templateId = new TemplateID(new ID("{C280DFD4-E3D4-4121-BA28-64DB2D9BE9B4}"));
            var database = Sitecore.Configuration.Factory.GetDatabase("master");
            var parentItemfromMaster = database.GetItem(contextItem.ID);
            using(new SecurityDisabler())
            {
                Item newCommentItem = parentItemfromMaster.Add(UserName, templateId);
                newCommentItem.Editing.BeginEdit();
                newCommentItem["UserName"] = UserName;
                newCommentItem["UserComment"] = UserComment;
                newCommentItem.Editing.EndEdit();

                Database master = Sitecore.Configuration.Factory.GetDatabase("master");
                Database web = Sitecore.Configuration.Factory.GetDatabase("web");

                PublishOptions publishOptions = new PublishOptions(master,
                                        web,
                                        PublishMode.SingleItem,
                                        newCommentItem.Language,
                                        System.DateTime.Now);
                Publisher publisher = new Publisher(publishOptions);
                publisher.Options.RootItem = newCommentItem;
                publisher.Options.Deep = true;
                publisher.Publish();

            }
            return View("ThankYouPage");
        }
    }
}