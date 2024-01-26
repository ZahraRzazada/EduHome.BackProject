using EduHome.Core.Entities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;
using EduHome.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduHome.Data.ServiceRegisterations
{
	public static class DataAccessServiceRegisterExtention
	{
		public static void DataAccessServiceRegister(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<EduDbContext>(opt =>
			{
				opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<ITestimoialRepository, TestimoialRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IDegreeRepository, DegreeRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IHobbyRepository, HobbyRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddIdentity<AppUser, IdentityRole>(
                opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                   
                    opt.SignIn.RequireConfirmedEmail = false;
                    
                })
                .AddEntityFrameworkStores<EduDbContext>().AddDefaultTokenProviders();
           
        }
    }
}

