using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using company_website.Models;
using company_website.dto;

namespace company_website.Controllers
{
    [Auth]
    public class PostsController : Controller
    {
        private readonly CompanyDbContext _context;

        public PostsController(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? filter, string? search)
        {
            IQueryable<Post> query = _context.Posts.Include(e => e.Category).Include(e => e.UserAccount);

            if (filter != null && filter != 0)
            {
                query = query.Where(a => a.Category.Id == filter);
            }
            else filter = 0;

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToUpper();
                query = query.Where(a =>
                     (
                         (a.Title ?? string.Empty).ToUpper() + " " +
                         (a.Content ?? string.Empty).ToUpper() + " " +
                         (a.Abstract ?? string.Empty).ToUpper() + " " +
                         (a.Category.Name ?? string.Empty).ToUpper() + " " +
                         (a.CreateDate.HasValue ? a.CreateDate.Value.ToString().ToUpper() : string.Empty) + " " +
                         (a.ModifyDate.HasValue ? a.ModifyDate.Value.ToString().ToUpper() : string.Empty)
                     ).Contains(search) ||
                     (a.UserAccount.Username ?? string.Empty).ToUpper().Contains(search)
                 );
            }

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.filter = filter;
            var result = await query.ToListAsync();
            result = result.OrderByDescending(a => a.ModifyDate).ToList();
            return View(result);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.UserAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Thumbnail,Content,Abstract")] PostDto model)
        {

            if (ModelState.IsValid)
            {
                byte[] thumbnailData = null;

                if (model.Thumbnail != null && model.Thumbnail.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Thumbnail.CopyToAsync(memoryStream);
                        thumbnailData = memoryStream.ToArray();
                    }
                }

                // Tạo đối tượng mới từ ViewModel


                //----- NHO LAY USER ---------

                var username = HttpContext.Session.GetString("User");
                var user = _context.UserAccounts.Where(a => a.Username.Equals(username)).
                    FirstOrDefault();

                

                var newEntity = new Post
                {
                    CategoryId = model.CategoryId,
                    Title = model.Title,
                    Abstract = model.Abstract,
                    Content = model.Content,
                    Thumbnail = thumbnailData,
                    CreateDate = DateOnly.FromDateTime(DateTime.Now),
                    ModifyDate = DateOnly.FromDateTime(DateTime.Now),
                    UserAccount = user,
                    UserAccountId=user.Id
                };

                // Lưu đối tượng mới vào cơ sở dữ liệu
                _context.Add(newEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var dto = new PostDto
            {
                Id = post.Id,
                CategoryId = post.CategoryId,
                Title = post.Title,
                Abstract = post.Abstract,
                Content = post.Content,
                ThumbnailBase64 = post.Thumbnail != null ? Convert.ToBase64String(post.Thumbnail) : null,
            };
            ViewBag.Category = _context.Categories.ToList();
            return View(dto);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Title,Thumbnail,Content,Abstract")] PostDto model)
        {


            if (ModelState.IsValid)
            {
                var post = await _context.Posts.FindAsync(id);

                byte[] thumbnailData = null;

                if (model.Thumbnail != null && model.Thumbnail.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Thumbnail.CopyToAsync(memoryStream);
                        thumbnailData = memoryStream.ToArray();
                        post.Thumbnail = thumbnailData;
                    }
                }

                post.CategoryId = model.CategoryId;
                post.Title = model.Title;
                post.Abstract = model.Abstract;
                post.Content = model.Content;
                post.ModifyDate = DateOnly.FromDateTime(DateTime.Now);



                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.UserAccount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
