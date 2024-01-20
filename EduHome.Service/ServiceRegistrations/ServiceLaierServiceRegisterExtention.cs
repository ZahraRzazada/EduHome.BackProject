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
        }
	}
}

