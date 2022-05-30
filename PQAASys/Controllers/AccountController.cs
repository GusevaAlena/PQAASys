using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PQAASys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace PQAASys.Controllers
{
    public class AccountController : Controller
    {
        private readonly PQASysContext _db;
        public AccountController(PQASysContext db)
        {
            _db = db;
        }

        public IActionResult Register(int? id)
        {
            RegisterViewModel RVM = new RegisterViewModel()
            {
                User = new User(),
                PositionSelectList = _db.Positions.Select(i => new SelectListItem
                {
                    Text = i.PositionName,
                    Value = i.PositionId.ToString()
                }),
                DepartmentSelectList = _db.Departments.Select(i => new SelectListItem
                {
                    Text = i.DepartmentName,
                    Value = i.DepartmentId.ToString()
                })

            };

            if (id == null)
            {
                return View(RVM);
            }
            else
            {
                RVM.User = _db.Users.Find(id);
                if (RVM.User == null)
                {
                    return NotFound();
                }
                return View(RVM);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel RVM)
        {
            if (ModelState.IsValid)
            {        
                User user = null;
                //проверяем существует ли пользователь с данным логином
                
                user = _db.Users.FirstOrDefault(u => u.Login == RVM.User.Login);
                
                //если нет, создаем нового
                if (user == null)
                {                   
                      _db.Add(RVM.User);
                      _db.SaveChanges();
                      user = _db.Users.Where(u => u.Login == RVM.User.Login &&
                      u.PasswordHash == RVM.User.PasswordHash).FirstOrDefault();
                    
                    //если пользователь удачно добавлен в базу
                    if (user != null)
                    {                        
                        //Authenticate.SetAuthCookie(RVM, true);
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            RVM.PositionSelectList = _db.Positions.Select(i => new SelectListItem
            {
                Text = i.PositionName,
                Value = i.PositionId.ToString()
            });
            RVM.DepartmentSelectList = _db.Departments.Select(i => new SelectListItem
            {
                Text = i.DepartmentName,
                Value = i.DepartmentId.ToString()
            });
            return View(RVM);
        }
        [HttpGet("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginViewModel LVM, string returnUrl)
        {
            ViewData["returnUrl"]=returnUrl;
            User user = new User();
            user = _db.Users.Where(u => u.Login == LVM.UserName && u.PasswordHash == LVM.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("UserName", LVM.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, LVM.UserName));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Неверно введен логин или пароль");
            }
            return View("Login");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
