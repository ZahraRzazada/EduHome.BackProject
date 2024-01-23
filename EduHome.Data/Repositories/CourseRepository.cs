using System;
using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(EduDbContext context) : base(context)
        {

        }
    }
}

