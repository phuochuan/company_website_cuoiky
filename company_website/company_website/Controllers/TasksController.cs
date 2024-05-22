using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using company_website.Models;
using Task = company_website.Models.Task;
using Microsoft.IdentityModel.Tokens;
using company_website.dto;
using System.Runtime.Serialization;

namespace company_website.Controllers
{
    [Auth]
    public class TasksController : Controller
    {
        private readonly CompanyDbContext _context;

        public TasksController(CompanyDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string? filter, string? search, int? teamsize)
        {

            IQueryable<Task> query = _context.Tasks;
            if (filter != null && !filter.Equals("All"))
            {
                query = query.Where(a => a.Status.Equals(filter));
            }
            else
            {
                filter = "All";
            }
            if (teamsize == 3 || teamsize == 10 || teamsize == 11)
            {
                if (teamsize == 3)
                    query = query.Where(a => a.TeamSize <= 3);
                if (teamsize == 10)
                    query = query.Where(a => a.TeamSize <= 10 && a.TeamSize > 3);
                if (teamsize == 11)
                    query = query.Where(a => a.TeamSize > 10);
            }
            else teamsize = 0;
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper().Trim();
                query = query.Where(a =>
                    (a.Name + " " + a.TaskDescription + " " + a.ExpectedEndDate.Value.Month + "/" + a.ExpectedEndDate.Value.Day + "/" + a.ExpectedEndDate.Value.Year + " " + a.StartDate.Value.Month + "/" + a.StartDate.Value.Day + "/" + a.StartDate.Value.Year)
                    .ToUpper().Contains(search.ToUpper()));

            }

            ViewBag.Filter = filter;
            ViewBag.Filter02 = teamsize;
            var result = await query.ToListAsync();
            return View(result);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewBag.Status = new String[3] { "Finish", "Delayed", "InProgress" };

            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TaskDescription,ExpectedEndDate,StartDate,Status,Thumbail")] TaskDto task)
        {
            if (ModelState.IsValid)
            {
                byte[] thumbailDT = null;
                if (task.Thumbail != null && task.Thumbail.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await task.Thumbail.CopyToAsync(memoryStream);
                        thumbailDT = memoryStream.ToArray();
                    }
                }

                var df = new DateTimeFormat("dd/MM/yyyy");
                var ntask = new Models.Task()
                {
                    Name = task.Name,
                    TaskDescription = task.TaskDescription,
                    ExpectedEndDate = DateOnly.Parse(getDATE(task.ExpectedEndDate)),
                    StartDate = DateOnly.Parse(getDATE(task.StartDate)),
                    Status = task.Status,
                    Thumbail = thumbailDT
                    ,
                    TeamSize = 0
                };
                _context.Add(ntask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Status = new String[3] { "Finish", "Delayed", "InProgress" };
            return View(task);
        }

        private ReadOnlySpan<char> getDATE(string d)
        {
            if (string.IsNullOrWhiteSpace(d)) return (new DateOnly()).ToString();
            string[] s = d.Split("/");
            var str = "";
            for (int i = 2; i >= 0; i--)
            {
                str = str + s[i] + "";
                if (i != 0) str = str + "-";
            }
            return str;
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var taskDto = new TaskDto()
            {
                Id = task.Id,
                Name = task.Name,
                TaskDescription = task.TaskDescription,
                ExpectedEndDate = tranferFormatDay(task.ExpectedEndDate),
                StartDate = tranferFormatDay(task.StartDate),
                Status = task.Status,
                ThumbnailBase64 = task.ThumbnailBase64
            };
            IQueryable<Employee> employees = _context.Employees.Include(a => a.Department);
            var employeeIdsOnTask = await _context.Schedules
                                           .Where(a => a.TaskId == task.Id)
                                           .Select(a => a.EmployeeId)
                                           .ToListAsync();
            var employeeAvailableAllTime = await employees
                .Where(a => !_context.Schedules.Any(s => s.EmployeeId == a.Id)).Include(a => a.Department)
                .ToListAsync();

            var employeeOnTaskButAvailable = await _context.Schedules
                .Where(a => a.TaskId != task.Id && task.StartDate > a.Task.ExpectedEndDate)
                .Select(a => a.Employee)
                .Distinct()
                .Include(a => a.Department)
                .ToListAsync();


            var combinedEmployeeList = employeeAvailableAllTime.Union(employeeOnTaskButAvailable).ToList();

            var employeesOnThisTask = employees.Where(a => employeeIdsOnTask.Contains(a.Id)).Include(a => a.Department).ToList();

            ViewBag.EmployeesOnThisTask = employeesOnThisTask;
            ViewBag.EmployeesOutThisTask = combinedEmployeeList;
            return View(taskDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddToTask(int employeeId, int taskId)
        {
            if (!_context.Schedules.Any(s => s.EmployeeId == employeeId && s.TaskId == taskId))
            {
                var task = _context.Tasks.Where(a => a.Id == taskId).First();
                task.TeamSize = task.TeamSize + 1;
                _context.Schedules.Add(new Schedule { EmployeeId = employeeId, TaskId = taskId });
                _context.Update(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = taskId });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromTask(int employeeId, int taskId)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.TaskId == taskId);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                var task = _context.Tasks.Where(a => a.Id == taskId).First();
                task.TeamSize = task.TeamSize - 1;
                _context.Update(task);
                await _context.SaveChangesAsync();

            }

            // Redirect back to the action method to show the updated task view
            return RedirectToAction("Edit", new { id = taskId });
        }


        private bool isOnThisTask(int id1, int id2)
        {
            var count = _context.Schedules.Where(a => a.TaskId.Equals(id2) && a.EmployeeId.Equals(id1)).ToList().Count();
            if (count == 1)
                return true;
            return false;
        }

        private String tranferFormatDay(DateOnly? expectedEndDate)
        {
            if (expectedEndDate == null) return "";

            var d = expectedEndDate.ToString();
            var s = d.Split("/");
            var str = "";
            str = s[1] + "/" + s[0] + "/" + s[2];
            return str;

        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TaskDescription,ExpectedEndDate,StartDate,Status,Thumbail")] TaskDto task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                var ta = _context.Tasks.Where(a => a.Id == task.Id).First();
                if (ta != null)
                {
                    ta.Name = task.Name;
                    ta.TaskDescription = task.TaskDescription;
                    ta.ExpectedEndDate = DateOnly.Parse(getDATE(task.ExpectedEndDate));
                    ta.StartDate = DateOnly.Parse(getDATE(task.StartDate));
                    ta.Status = task.Status;
                    if (ta.Status != task.Status && task.Status.Equals("Finished"))
                        ta.FinishDate = new DateOnly();
                    byte[] thumbailDT = null;
                    if (task.Thumbail != null && task.Thumbail.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await task.Thumbail.CopyToAsync(memoryStream);
                            thumbailDT = memoryStream.ToArray();
                            ta.Thumbail = thumbailDT;

                        }
                    }
                    _context.Update(ta);
                    await _context.SaveChangesAsync();
                }
                ////////}
                ////////catch (DbUpdateConcurrencyException)
                ////////{
                ////////    if (!TaskExists(task.Id))
                ////////    {
                ////////        return NotFound();
                ////////    }
                ////////    else
                ////////    {
                ////////        throw;
                ////////    }
                ////////}
                ////////return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Edit", new { id = task.Id });
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
