﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using company_website.Models;

namespace company_website.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyDbContext _context;

        public EmployeesController(CompanyDbContext context)
        {
            _context = context;
        }

       




        // GET: /YourController/Search
        [HttpGet]
        public async Task<IActionResult> Index(int? filter, string? search)
        {
            IQueryable<Employee> query = _context.Employees.Include(e => e.Department).Include(e => e.UserAccount);

            if (filter != null && filter != 0)
            {
                query = query.Where(a => a.DepartmentId == filter);
            }
            else filter = 0;

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToUpper();
                query = query.Where(a => a.Fullname.ToUpper().Contains(search) ||
                                          //a.UserAccount.Username.ToUpper().Contains(search) ||
                                          a.IdNo.Contains(search)||
                                          EF.Functions.Like(a.Department.Description, $"%{search}%") ||
                                          EF.Functions.Like(a.DateOfBirth.ToString(), $"%{search}%") ||
                                          EF.Functions.Like(a.PhoneNumber.ToString(), $"%{search}%")
                                          );
            }

            var result = await query.ToListAsync();
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Department = departments;
            ViewBag.filter = filter;

            return View(result);
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.UserAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Department"] = _context.Departments;
            ViewData["UserAccount"] = _context.UserAccounts;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,Fullname,DateOfBirth,IdNo, PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department"] = _context.Departments;
            ViewData["UserAccount"] = _context.UserAccounts;
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Department"] = _context.Departments;
            
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentId,Fullname,DateOfBirth,IdNo, PhoneNumber")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department"] = _context.Departments;

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.UserAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}