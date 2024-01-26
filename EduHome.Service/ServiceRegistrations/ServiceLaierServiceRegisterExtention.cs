using EduHome.Service.ExternalServices.Implementations;
using EduHome.Service.ExternalServices.Interfaces;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EduHome.Service.ServiceRegistrations
{
	public static class ServiceLaierServiceRegisterExtention
	{
		public static void ServiceLaierServiceRegister(this IServiceCollection services)
		{
			services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IDegreeService, DegreeService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHobbyService, HobbyService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, EmailService>();

        }
	}
}

