using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBEntity;
using TBRepo;

namespace TravelBlog.Controllers
{
    public class DefaultController : Controller
    {
        private TBReposetory  repo = new TBReposetory();
        private UserRepository urepo = new UserRepository();
        private AdminRepository arepo = new AdminRepository();

        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Regester()
        {
            return View("Regester");
        }

        [HttpPost]
        public ActionResult Regester(FormCollection collection)
        {
            if(collection["fname"]!="" && collection["lname"] != "" && collection["email"] != "" && collection["phone"] != "" && collection["address"] != "" && collection["password"] != "")
            {
                User user = new User();
                user.FirstName = collection["fname"];
                user.LastName = collection["lname"];
                user.Email = collection["email"];
                user.Phone = collection["phone"];
                user.Division = collection["division"];
                user.Address = collection["address"];
                user.Password = collection["password"];

                bool existUser = repo.IsExistUser(user.Email);
                if (!existUser)
                {
                    this.repo.Regestration(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("User with this email is already exist");
                }
            }
            else
            {
                return Content("Please fill all the field");
            }
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public ActionResult UserLogin(FormCollection collection)
        {
            User user = new User();
            user = this.repo.login(collection["email"], collection["password"]);
            Session["uModel"] = user;
            //bool valid = urepo.Validate(user);
            if (user!=null)
            {
                Session["Email"] = user.Email;
                Session["Id"] = user.Id;
                Session["name"] = user.FirstName;
                return RedirectToAction("Index", "User");
            }
            else
            {
                return Content("Invalid username or password");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            bool valid = arepo.Validate(admin);
            if (valid)
            {
                Session["AdminEmail"] = admin.Email;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return Content("Invalid username or password");
            }
        }

        public ActionResult Divisions()
        {
            return View("Divisions");
        }
    }
}