using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(EduDbContext context) : base(context)
        {

        }
    }
}

