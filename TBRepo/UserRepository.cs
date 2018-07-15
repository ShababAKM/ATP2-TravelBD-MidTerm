using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using TBEntity;

namespace TBRepo
{
    public class UserRepository
    {
        private TBDBContext context = new TBDBContext();

        public bool Validate(User user)
        {
            User validUser = this.context.Users.SingleOrDefault(a => a.Email == user.Email && a.Password == user.Password);
            return validUser != null;
        }

        public User Info(string email)
        {
            return this.context.Users.SingleOrDefault(u => u.Email == email);
        }

        public int Update(User user)
        {
            User userToUpdate = this.Info(user.Email);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Division = user.Division;
            userToUpdate.Address = user.Address;
            return this.context.SaveChanges();
        }

        public bool CheckOldPass(string email, string oldPass)
        {
            var q = from p in context.Users
                    where p.Email == email
                    && p.Password == oldPass
                    select p;

            if(q.Any()) return true;
            else return false;
        }
        
        public int ChangePass(string email, string oldPass)
        {
            User userToUpdate = this.Info(email);
            userToUpdate.Password = oldPass;
            return this.context.SaveChanges();
        }

        public List<User> GetAllUser()
        {
            return this.context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return this.context.Users.SingleOrDefault(p => p.Id == id);
        }

        public int DeleteUser(int id)
        {
            User userToDelete = this.GetUser(id);
            this.context.Users.Remove(userToDelete);
            return this.context.SaveChanges();
        }
    }
}
