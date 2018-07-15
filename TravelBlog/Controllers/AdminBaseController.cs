using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBlog.Controllers
{
    public class AdminBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["AdminEmail"] == null)
            {
                Response.Redirect("http://localhost:7626/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}