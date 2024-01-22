using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Data.Contexts;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Controllers
{
    public class BlogController : Controller
    {

        readonly EduDbContext _context;
        readonly IBlogService _blogService;


        public BlogController(EduDbContext context,IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogGetDto> Blogs = await _blogService.GetAllAsync();
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
                .Include(x => x.TagBlogs)
                .ThenInclude(x => x.Blog)
                .Select(tag => new {Name = tag.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categorys = _context.Categories.Where(m=>!m.IsDeleted)
                .Include(m=>m.Blogs)
               .Select(cate => new { Name= cate.Name,Count=cate.Blogs.Where(m=>!m.IsDeleted).Count()}).AsNoTrackingWithIdentityResolution();

            return View(Blogs);
        }
        public async Task<IActionResult> Detail(int id)
        {

            var blogGetDto = await _blogService.GetAsync(id);
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
                   .Include(x => x.TagBlogs)
                   .ThenInclude(x => x.Blog)
                   .Select(tag => new { Name = tag.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categorys = _context.Categories.Where(m => !m.IsDeleted)
            .Include(m => m.Blogs)
            .Select(cate => new { Name = cate.Name, Count = cate.Blogs.Where(m => !m.IsDeleted).Count() }).AsNoTrackingWithIdentityResolution();

            return View(blogGetDto);
        }
    }
}

