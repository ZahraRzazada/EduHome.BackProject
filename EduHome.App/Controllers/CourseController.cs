using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Data.Contexts;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Controllers
{
    public class CourseController : Controller
    {
        readonly EduDbContext _context;
        readonly ICourseService _courseService;


        public CourseController(EduDbContext context, ICourseService courseService)
        {
            _context = context;
            _courseService = courseService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CourseGetDto> courses = await _courseService.GetAllAsync();
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
                .Include(x => x.TagBlogs)
                .ThenInclude(x => x.Blog)
                .Select(tag => new { Name = tag.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categorys = _context.Categories.Where(m => !m.IsDeleted)
                .Include(m => m.Blogs)
               .Select(cate => new { Name = cate.Name, Count = cate.Blogs.Where(m => !m.IsDeleted).Count() }).AsNoTrackingWithIdentityResolution();

            return View(courses);
           
        }
        public async Task<IActionResult> Detail(int id)
        {
            var coursegetdto = await _courseService.GetAsync(id);
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
                   .Include(x => x.TagBlogs)
                   .ThenInclude(x => x.Blog)
                   .Select(tag => new { Name = tag.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categorys = _context.Categories.Where(m => !m.IsDeleted)
            .Include(m => m.Blogs)
            .Select(cate => new { Name = cate.Name, Count = cate.Blogs.Where(m => !m.IsDeleted).Count() }).AsNoTrackingWithIdentityResolution();

            return View(coursegetdto);
        }
    }
}

