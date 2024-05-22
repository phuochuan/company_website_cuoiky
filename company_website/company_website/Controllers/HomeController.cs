using Azure.Core.GeoJson;
using company_website.dto;
using company_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace company_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyDbContext _context;

       
        public HomeController(ILogger<HomeController> logger, CompanyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.company=_context.Companies.FirstOrDefault();
            var recentTasks = _context.Tasks
                .Where(a => a.StartDate <= DateOnly.FromDateTime(DateTime.Today) &&
                            (a.ExpectedEndDate >= DateOnly.FromDateTime(DateTime.Today) || a.Status.Equals("InProgress")))
                .ToList();
            ViewBag.recentTask = recentTasks;
            var recentTaskIds= recentTasks.Select(a=>a.Id).ToList();
            var recentEmphoyees = _context.Schedules.Where(a => recentTaskIds.Contains((int)a.TaskId))
                .Select(a => new EmphoyeeDto()
                {
                    emName = a.Employee.Fullname,
                    taskName = a.Task.Name,
                    taskId=a.TaskId,
                    emId=a.Id
                }).ToList() ;
            ViewBag.recentEmphoyees = recentEmphoyees;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
