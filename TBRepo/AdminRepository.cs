using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBEntity;

namespace TBRepo
{
    public class AdminRepository
    {
        private TBDBContext context = new TBDBContext();

        public bool Validate(Admin admin)
        {
            Admin validAdmin = this.context.Admins.SingleOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);
            return validAdmin != null;
        }

        public int Insert(Admin admin)
        {
            this.context.Admins.Add(admin);
            return this.context.SaveChanges();
        }

        public bool IsExistAdmin(string email)
        {
            var q = from p in context.Admins
                    where p.Email == email
                    select p;
            if (q.Any()) return true;
            else return false;
        }

        public Admin Info(string email)
        {
            return this.context.Admins.SingleOrDefault(u => u.Email == email);
        }

        public int Update(Admin admin)
        {
            Admin adminToUpdate = this.Info(admin.Email);
            adminToUpdate.FirstName = admin.FirstName;
            adminToUpdate.LastName = admin.LastName;
            adminToUpdate.Phone = admin.Phone;
            adminToUpdate.Division = admin.Division;
            adminToUpdate.Address = admin.Address;
            return this.context.SaveChanges();
        }
        
        public bool CheckOldPass(string email, string oldPass)
        {
            var q = from p in context.Admins
                    where p.Email == email
                    && p.Password == oldPass
                    select p;

            if (q.Any()) return true;
            else return false;
        }

        public int ChangePass(string email, string oldPass)
        {
            Admin adminToUpdate = this.Info(email);
            adminToUpdate.Password = oldPass;
            return this.context.SaveChanges();
        }

        public List<Admin> GetAllAdmin()
        {
            return this.context.Admins.ToList();
        }

        public Admin GetAdmin(int id)
        {
            return this.context.Admins.SingleOrDefault(p => p.Id == id);
        }

        public int DeleteAdmin(int id)
        {
            Admin adminToDelete = this.GetAdmin(id);
            this.context.Admins.Remove(adminToDelete);
            return this.context.SaveChanges();
        }
    }
}
