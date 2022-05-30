using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PQAASys.Controllers
{
    public class StandartController : Controller
    {
        private readonly PQASysContext _db;
        public StandartController(PQASysContext db)
        {
            _db = db;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string SearchString1, string SearchString2)
        {
            IEnumerable<Standart> objList = _db.Standarts;
            if (SearchString1 != null)
            {
                objList = objList.Where(u => u.StandartSeries.Contains(SearchString1));
            }
            if (SearchString2 != null)
            {
                objList = objList.Where(u => u.StandartNumber.Contains(SearchString2));
            }
            return View(objList);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Standart standart = new Standart();
            return View(standart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Standart standart)
        {
            if (ModelState.IsValid)
            {
                _db.Add(standart);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Standart standart = new Standart();
            standart = _db.Standarts.Where(i => i.Standart_id == id).FirstOrDefault();
            return View(standart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Standart standart)
        {
            _db.Standarts.Update(standart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {           
            Standart standart = _db.Standarts.Find(id);
            return View(standart);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Standart standart = _db.Standarts.Find(id);
            _db.Standarts.Remove(standart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
