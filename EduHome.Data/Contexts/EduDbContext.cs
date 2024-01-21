using System;
using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Data.Contexts
{
    public class EduDbContext : DbContext
    {
        public EduDbContext(DbContextOptions<EduDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<TeacherFaculty> TeacherFaculties { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }


        
    }
}
