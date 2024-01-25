using EduHome.App.ViewModels;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Controllers
{
    public class AboutController : Controller
    {
        readonly ITeacherService _teacherService;
        readonly ITestimonialService _testimonialService;

        public AboutController(ITeacherService teacherService, ITestimonialService testimonialService)
        {
            _teacherService = teacherService;
            _testimonialService = testimonialService;
        }
        public async Task<IActionResult> Index()
        {
            AboutViewModel aboutViewModel = new AboutViewModel();
            aboutViewModel.Teachers = await _teacherService.GetAllAsync();
            aboutViewModel.Testimoials = await _testimonialService.GetAllAsync();

            return View(aboutViewModel);
        }
    }
}

