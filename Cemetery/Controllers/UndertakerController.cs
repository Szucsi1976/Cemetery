using Microsoft.AspNetCore.Mvc;
using Cemetery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cemetery.Utility;

namespace Cemetery.Controllers
{
    [Authorize(Roles =Helper.Admin)]
    public class UndertakerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UndertakerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Undertaker> objList = _db.Undertakers;
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
        public IActionResult Create(Undertaker obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Undertakers.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewBag.ErrorMessage = Utility.Helper.CreateErrorMessage;
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Undertakers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(Undertaker obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Undertakers.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewBag.ErrorMessage = Utility.Helper.EditErrorMessage;
            }
            return View(obj);
        }

        // Get Delete
        public IActionResult Delete(int? id, bool? saveChangesError)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = Utility.Helper.DeleteErrorMessage;
            }
            var obj = _db.Undertakers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? UndertakerId)
        {
            var obj = _db.Undertakers.Find(UndertakerId);
            if (obj == null)
            {
                return NotFound();
            }
            try
            {
                _db.Undertakers.Remove(obj);
                _db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = UndertakerId, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
