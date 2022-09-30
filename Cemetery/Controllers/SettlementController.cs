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
    public class SettlementController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SettlementController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Settlement> objList = _db.Settlements;
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
        public IActionResult Create(Settlement obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Settlements.Add(obj);
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
            var obj = _db.Settlements.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(Settlement obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Settlements.Update(obj);
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
            var obj = _db.Settlements.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? SettlementId)
        {
            var obj = _db.Settlements.Find(SettlementId);
            if (obj == null)
            {
                return NotFound();
            }
            try
            {
                _db.Settlements.Remove(obj);
                _db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = SettlementId, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
