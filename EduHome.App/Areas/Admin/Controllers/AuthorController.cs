using EduHome.Core.DTOS;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class AuthorController : Controller
    {
        readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _authorService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _authorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _authorService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, AuthorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _authorService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }

}