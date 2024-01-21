using EduHome.Core.Repositories;
using EduHome.Data.Contexts;
using EduHome.Data.Repositories;
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

        }
    }
}

