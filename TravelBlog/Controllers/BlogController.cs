using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBEntity;
using TBRepo;

namespace TravelBlog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private TBReposetory repo = new TBReposetory();
        private UserRepository urepo = new UserRepository();
        private BlogRepo brepo = new BlogRepo();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SBlog()
        {
            return View(this.brepo.GetBlog());

        }
        public ActionResult ABlog()
        {

            return View(this.brepo.GetBlog());
        }
        public ActionResult SrcBlog(FormCollection collection)
        {
            string searchString = collection["SResult"];
            return View(this.brepo.SearchBlog(searchString));
        }
        public ActionResult SaveBlog(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog();
                blog.Title = collection["name"];
                blog.Address = collection["email"];
                blog.blog = collection["blog"];
                blog.WriterName = (string)Session["name"];
                blog.WriterId=(int)Session["Id"];
                Session["ublog"] = blog;
                this.brepo.BlogUpdate(blog);
                return RedirectToAction("UBlog","User");
            }
            else
            {
                collection["name"] = "";
                return View("Register");
            }
        }
        [HttpGet]
        public ActionResult BlogDetails(int id)
        {
            Blog a = this.brepo.GetBlog(id);
            return View(a);
        }

        [HttpGet]
        public ActionResult DeleteBlog(int id)
        {
            Blog a = this.brepo.GetBlog(id);
            return View(a);
        }

        [HttpPost]
        [ActionName("DeleteBlog")]
        public ActionResult DeleteConf(int id)
        {
            this.brepo.DeleteBlog(id);
            return RedirectToAction("Index");
        }
    }
}