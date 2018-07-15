using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBEntity;
using TBRepo;

namespace TravelBlog.Controllers
{
    public class AdminController : AdminBaseController
    {
        private AdminRepository repo = new AdminRepository();
        private UserRepository urepo = new UserRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            Admin admin = new Admin();
            admin = this.repo.Info(Session["AdminEmail"].ToString());
            return View(admin);
        }

        [HttpGet]
        public ActionResult Edit(Admin admin)
        {
            admin = this.repo.Info(Session["AdminEmail"].ToString());
            return View(admin);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            if(collection["fname"] != "" && collection["lname"] != "" && collection["phone"] != "" && collection["address"] != "")
            {
                Admin admin = new Admin();
                admin.Email = Session["AdminEmail"].ToString();
                admin.FirstName = collection["fname"];
                admin.LastName = collection["lname"];
                admin.Phone = collection["phone"];
                admin.Division = collection["division"];
                admin.Address = collection["address"];
                this.repo.Update(admin);
                return RedirectToAction("Profile");
            }
            else
            {
                return Content("Please fill all the field");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(Admin admin)
        {
            admin = this.repo.Info(Session["AdminEmail"].ToString());
            return View(admin);
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            string newPass, confirmNewPass;
            User user = new User();
            user.Email = Session["Email"].ToString();
            user.Password = collection["cpass"];
            newPass = collection["npass"];
            confirmNewPass = collection["cnpass"];
            bool correctOldPass = repo.CheckOldPass(user.Email, user.Password);
            if (correctOldPass)
            {
                if (newPass == confirmNewPass)
                {
                    this.repo.ChangePass(Session["AdminEmail"].ToString(), newPass);
                }
                else
                {
                    return Content("Password doesn't match");
                }
            }
            else
            {
                return Content("Please enter the current password correctly");
            }
            return RedirectToAction("ChangePassword");
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(FormCollection collection)
        {
            if(collection["fname"] != "" && collection["lname"] != "" && collection["email"] != "" && collection["phone"] != "" && collection["address"] != "" && collection["password"] != "")
            {
                Admin admin = new Admin();
                admin.FirstName = collection["fname"];
                admin.LastName = collection["lname"];
                admin.Email = collection["email"];
                admin.Phone = collection["phone"];
                admin.Division = collection["division"];
                admin.Address = collection["address"];
                admin.Password = collection["password"];
                bool existAdmin = repo.IsExistAdmin(admin.Email);
                if (!existAdmin)
                {
                    this.repo.Insert(admin);
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Admin with this email is already exist");
                }
            }
            else
            {
                return Content("Please fill all the field");
            }
            
        }

        public ActionResult UserList()
        {
            return View(this.urepo.GetAllUser());
        }

        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            User u = this.urepo.GetUser(id);
            return View(u);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            User u = this.urepo.GetUser(id);
            return View(u);
        }

        [HttpPost][ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.urepo.DeleteUser(id);
            return RedirectToAction("UserList");
        }

        public ActionResult AdminList()
        {
            return View(this.repo.GetAllAdmin());
        }

        [HttpGet]
        public ActionResult AdminDetails(int id)
        {
            Admin a = this.repo.GetAdmin(id);
            return View(a);
        }

        [HttpGet]
        public ActionResult DeleteAdmin(int id)
        {
            Admin a = this.repo.GetAdmin(id);
            return View(a);
        }

        [HttpPost]
        [ActionName("DeleteAdmin")]
        public ActionResult DeleteConf(int id)
        {
            this.repo.DeleteAdmin(id);
            return RedirectToAction("AdminList");
        }

        public ActionResult SignOut()
        {
            Session.Remove("AdminEmail");
            return RedirectToAction("Index", "Default");
        }
    }
}