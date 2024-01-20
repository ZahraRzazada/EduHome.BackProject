using System;
using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Data.Contexts
{
	public class EduDbContext:DbContext
	{
		public EduDbContext(DbContextOptions<EduDbContext> options):base(options)
		{

		}
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
	}
}

