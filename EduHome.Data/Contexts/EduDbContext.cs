using System;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Data.Contexts
{
    public class EduDbContext : IdentityDbContext<AppUser>
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
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TagCourse> TagCourses { get; set; }
        public DbSet<TagBlog> TagBlogs { get; set; }
        public DbSet<Notice> Notices { get; set; }




    }

}
