using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBEntity;

namespace TBRepo
{
    public class BlogRepo
    {
        private TBDBContext context = new TBDBContext();
        public int BlogUpdate(Blog blog)
        {
            this.context.Blogs.Add(blog);
            return this.context.SaveChanges();
        }
        public List<Blog> GetBlog()
        {
            return this.context.Blogs.ToList();
        }
        public List<Blog> SearchBlog(string searchString)
        {
            return this.context.Blogs.Where(p => p.Title.Contains(searchString)).ToList();
        }
        public Blog GetBlog(int id)
        {
            return this.context.Blogs.SingleOrDefault(p => p.Id == id);
        }

        public int DeleteBlog(int id)
        {
            Blog BlogToDelete = this.GetBlog(id);
            this.context.Blogs.Remove(BlogToDelete);
            return this.context.SaveChanges();
        }
    }
}
