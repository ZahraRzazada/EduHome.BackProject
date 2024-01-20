using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class TestimoialRepository : Repository<Testimonial>, ITestimoialRepository
    {
        public TestimoialRepository(EduDbContext context) : base(context)
        {

        }
    }
}

