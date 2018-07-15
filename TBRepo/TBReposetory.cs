using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBEntity;

namespace TBRepo
{
    public class TBReposetory
    {
        private TBDBContext context = new TBDBContext();

        public int Regestration(User user)
        {
            this.context.Users.Add(user);
            return this.context.SaveChanges();
        }

        public bool IsExistUser(string email)
        {
            var q = from p in context.Users
                    where p.Email == email
                    select p;
            if (q.Any()) return true;
            else return false;
        }
        public User login(string email, string pass)
        {
            return this.context.Users.SingleOrDefault(p => p.Email == email && p.Password == pass);
        }
    }
}
