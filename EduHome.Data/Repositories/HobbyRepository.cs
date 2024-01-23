using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class HobbyRepository : Repository<Hobby>, IHobbyRepository
    {
        public HobbyRepository(EduDbContext context) : base(context)
        {

        }
    }
}

