using System.Diagnostics;
using EduHome.App.ViewModels;
using EduHome.Core.DTOS;
using EduHome.Data.Contexts;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using EduHome.Models;

namespace EduHome.App.Controllers;

public class HomeController : Controller
{
  
    readonly ISliderService _sliderService;
    readonly ITestimonialService _testimonialService;
    readonly ICourseService _courseService;
    readonly IBlogService _blogService;

    public HomeController(ISliderService sliderService, ITestimonialService testimonialService, ICourseService courseService, IBlogService blogService)
    {
        _sliderService = sliderService;
        _testimonialService = testimonialService;
        _courseService = courseService;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        homeViewModel.Sliders = await _sliderService.GetAllAsync();
        homeViewModel.Testimoials = await _testimonialService.GetAllAsync();
        homeViewModel.Courses = await _courseService.GetAllAsync();
        homeViewModel.Blogs = await _blogService.GetAllAsync();
        return View(homeViewModel);
    }
}
