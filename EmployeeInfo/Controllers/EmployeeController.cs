using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.ShowLogin = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(Login ob)
        {
            if (ModelState.IsValid)
            {
                Ad_Login obj1 = _db.Admins_Login.Find(ob.Id);
                if (obj1 != null)
                {
                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.NameIdentifier,obj1.Id.ToString()),
                                    new Claim(ClaimTypes.Role, "Admin"),
                                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("AllEmployees");
                }
                Employee_Login obj2 = _db.Employees_Login.Find(ob.Id);
                if (obj2 != null)
                {
                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.NameIdentifier,obj2.Id.ToString()),
                                    new Claim(ClaimTypes.Role, "Employee"),
                                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    if ((_db.Employees.FirstOrDefault(u => u.EmployeeId == ob.Id)) != null)
                    {
                        return RedirectToAction("Details2", new { id = ob.Id });
                    }
                    return RedirectToAction("Create");
                }
                return NotFound();
            }
            return View(ob);
            
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //AllEmployees
        public IActionResult AllEmployees()
        {
            
            ViewBag.ShowLogin = false;
            IEnumerable<Employee> objList = _db.Employees;
            return View(objList);
        }
        //GET_REGISTRATION
        public IActionResult Registration()
        {
            ViewBag.ShowLogin = true;
            return View();
        }
        //POST-REGISTRATION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Login ob)
        {
            Employee_Login obj = new Employee_Login
            {
                Id = ob.Id,
                Password = ob.Password
            };
            if (ModelState.IsValid)
            {
                _db.Employees_Login.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ob);
        }
        //CREATE
        public IActionResult Create()
        {
            
            ViewBag.ShowLogin = false;
            return View();
        }
        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee ob)
        {

            if (ModelState.IsValid)
            {
                _db.Employees.Add(ob);
                _db.SaveChanges();
                return RedirectToAction("Details2" , new { id = ob.EmployeeId });
            }
            return View(ob);
        }
        //DETAILS
        public IActionResult Details(int? id)
        {
            var ob = _db.Employees.Find(id);
            if (ob == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogin = false;
            return View(ob);
        }

        //DETAILS2
        public IActionResult Details2(int? id)
        {
            var ob = _db.Employees.Find(id);
            if (ob == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogin = false;
            return View(ob);
        }

        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            var ob = _db.Employees.Find(id);
            if (ob == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogin = false;
            return View(ob);
        }
        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee ob)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(ob);
                _db.SaveChanges();
                return RedirectToAction("AllEmployees");
            }
            return View(ob);
        }
        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            var ob = _db.Employees.Find(id);
            if (ob == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogin = false;
            return View(ob);
        }
        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Employee ob)
        {
            if (ob != null)
            {
                _db.Employees.Remove(ob);
                _db.SaveChanges();
                return RedirectToAction("AllEmployees");
            }
            return NotFound();
        }

    }
}
