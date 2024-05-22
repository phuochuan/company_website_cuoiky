using company_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace company_website.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login

        private readonly CompanyDbContext _context;

        public AccountController(CompanyDbContext context)
        {
            _context = context;
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            HttpContext.Session.SetString("User", username);

            var user=_context.UserAccounts.Where(user=>user.Username == username).FirstOrDefault();
            if (user != null)
            {
                if (password == user.Password )
                {
                    if (user.RoleId==1)
                    {
                        if (HttpContext.Session != null)
                        {
                            HttpContext.Session.SetString("User", username);
                            return RedirectToAction("Index", "Home");
                        }
                       
                    }
                    else
                        ViewBag.Message = "Bạn không có quyền truy cập.";
                }
                else
                ViewBag.Message = "Mật khẩu không đúng.";
            }
            else
                ViewBag.Message = "Tên đăng nhập  không đúng.";
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return RedirectToAction("Login", "Account");
        }
    }
}
