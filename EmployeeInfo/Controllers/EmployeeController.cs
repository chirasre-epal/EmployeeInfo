using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeInfo.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login ob)
        {
            Login obj = _db.Admins_Login.Find(ob.Id);
            if (obj!=null)
            {
                return RedirectToAction("AllEmployees");
            }
            obj = _db.Employees_Login.Find(ob.Id);
            if (obj!=null)
            {
                if((_db.Employees.FirstOrDefault(u => u.EmployeeId == ob.Id)) != null)
                {
                    return RedirectToAction("Details", new { id = ob.Id });
                }
                return RedirectToAction("Create");
            }
            return NotFound();
        }
        //AllEmployees
        public IActionResult AllEmployees()
        {
            IEnumerable<Employee> objList = _db.Employees;
            return View(objList);
        }
        //GET_REGISTRATION
        public IActionResult Registration()
        {
            return View();
        }
        //POST-REGISTRATION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Login ob)
        {
            Employee_Login obj = new Employee_Login();
            obj.Id = ob.Id;
            obj.Password = ob.Password;
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
                return RedirectToAction("Details" , new { id = ob.EmployeeId });
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
