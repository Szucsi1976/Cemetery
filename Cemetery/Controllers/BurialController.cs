using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cemetery.Models;
using System.Collections.Generic;
using System.Linq;
using Cemetery.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Cemetery.Utility;

namespace Cemetery.Controllers
{
    [Authorize(Roles = Helper.Admin)]
    public class BurialController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BurialController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Burial> objList = _db.Burials;
            foreach (var obj in objList)         // Ez kell a típusok megjelenítéséhez
            {
                obj.Undertaker = _db.Undertakers.FirstOrDefault(u => u.UndertakerId == obj.BurialUndertakerId);
            }
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            BurialVM burialVm = new BurialVM()
            {
                TypeDropDownUndertaker = _db.Undertakers.Select(i => new SelectListItem
                {
                    Text = i.UndertakerName,
                    Value = i.UndertakerId.ToString()
                }),
            };
            return View(burialVm);
        }
        
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BurialVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Burials.Add(obj.Burial);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            BurialVM burialVm = new BurialVM()
            {
                TypeDropDownUndertaker = _db.Undertakers.Select(i => new SelectListItem
                {
                    Text = i.UndertakerName,
                    Value = i.UndertakerId.ToString()
                }),
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }

            burialVm.Burial = _db.Burials.Find(id);
            if (burialVm.Burial == null)
            {
                return NotFound();
            }

            return View(burialVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(BurialVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Burials.Update(obj.Burial);
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
            var obj = _db.Burials.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? Funeralid)
        {
            var obj = _db.Burials.Find(Funeralid);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Burials.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

