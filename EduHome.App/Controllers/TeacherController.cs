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
    public class TeacherController : Controller
    {
         readonly EduDbContext _context;
        readonly IDegreeService _degreeService;
        readonly IFacultyService _facultyService;
        readonly IPositionService _positionService;
        readonly ITeacherService _teacherService;

        public TeacherController(IDegreeService degreeService, IFacultyService facultyService, IPositionService positionService, ITeacherService teacherService, EduDbContext context)
        {
            _degreeService = degreeService;
            _facultyService = facultyService;
            _positionService = positionService;
            _teacherService = teacherService;
            _context = context;
        }



        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Faculties = _context.Faculties.Where(x => !x.IsDeleted)
    .Include(x => x.TeacherFaculties)
    .ThenInclude(x => x.Teacher)
    .Select(fac => new { Name = fac.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Hobbies = _context.Hobbies.Where(m => !m.IsDeleted)
                .Include(m => m.TeacherHobbies)
               .Select(hob => new { Name = hob.Name }).AsNoTrackingWithIdentityResolution();
            IEnumerable<TeacherGetDto> Teachers = await _teacherService.GetAllAsync();
            return View(Teachers);
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Faculties = _context.Faculties.Where(x => !x.IsDeleted)
.Include(x => x.TeacherFaculties)
.ThenInclude(x => x.Teacher)
.Select(fac => new { Name = fac.Name }).AsNoTrackingWithIdentityResolution();
            ViewBag.Hobbies = _context.Hobbies.Where(m => !m.IsDeleted)
                .Include(m => m.TeacherHobbies)
               .Select(hob => new { Name = hob.Name }).AsNoTrackingWithIdentityResolution();
            var taecherGetDto = await _teacherService.GetAsync(id);
            return View(taecherGetDto);
        }
    }
}

