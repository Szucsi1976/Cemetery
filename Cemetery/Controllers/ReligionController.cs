using Microsoft.AspNetCore.Mvc;
using Cemetery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cemetery.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Cemetery.Controllers
{
    [Authorize(Roles = Helper.Admin)]
    public class ReligionController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ReligionController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Religion> objList = _db.Religions;
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Religion obj)
        {
            if (ModelState.IsValid)
            {
                _db.Religions.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Religions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(Religion obj)
        {
            if (ModelState.IsValid)
            {
                _db.Religions.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            var obj = _db.Religions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? ReligionId)
        {
            var obj = _db.Religions.Find(ReligionId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Religions.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
