using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        readonly ICourseService _courseService;
        readonly ITagService _tagService;
        readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ITagService tagService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _courseService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
       
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categorys = await _categoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoursePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View();
            }
            var response = await _courseService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categorys = await _categoryService.GetAllAsync();

            ViewBag.Tags = await _tagService.GetAllAsync();
            return View(await _courseService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CoursePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
         
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View(await _courseService.GetAsync(id));
            }
            var response = await _courseService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
               
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View(await _courseService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _courseService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

