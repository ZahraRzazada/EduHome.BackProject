using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(EduDbContext context) : base(context)
        {

        }
    }
}

