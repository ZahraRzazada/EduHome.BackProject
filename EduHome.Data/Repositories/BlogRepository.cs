using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(EduDbContext context) : base(context)
        {

        }
    }
}

