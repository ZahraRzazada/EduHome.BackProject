using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly IAuthorService _authorService;
        readonly ITagService _tagService;
        readonly ICategoryService _categoryService;


        public BlogController(IBlogService BlogService, IAuthorService AuthorService, ITagService tagService, ICategoryService categoryService)
        {
            _blogService = BlogService;
            _authorService = AuthorService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _blogService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categorys = await _categoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View();
            }
            var response = await _blogService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categorys = await _categoryService.GetAllAsync();
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View(await _blogService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View(await _blogService.GetAsync(id));
            }
            var response = await _blogService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Categorys = await _categoryService.GetAllAsync();
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View(await _blogService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _blogService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

