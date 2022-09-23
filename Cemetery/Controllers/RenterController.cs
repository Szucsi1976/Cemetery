using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cemetery.Models;
using Cemetery.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cemetery.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Cemetery.Controllers
{
    [Authorize(Roles = Helper.Admin)]
    public class RenterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RenterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Renter> objList = _db.Renters;
            foreach (var obj in objList)         // Ez kell a típusok megjelenítéséhez
            {
                obj.Settlement= _db.Settlements.FirstOrDefault(u => u.SettlementId == obj.RenterSettlementId);
            }
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            RenterVM renterVM = new RenterVM()
            {
                Renter = new Renter(),
                TypeDropDown = _db.Settlements.Select(i => new SelectListItem
                {
                    Text = i.Station + "  " + i.PostalCode.ToString(),
                    Value = i.SettlementId.ToString()
                })
            };
            return View(renterVM);
        }
        
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RenterVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Renters.Add(obj.Renter);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            RenterVM renterVM = new RenterVM()
            {
                Renter = new Renter(),
                TypeDropDown = _db.Settlements.Select(i => new SelectListItem
                {
                    Text = i.Station + "  " + i.PostalCode.ToString(),
                    Value = i.SettlementId.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            renterVM.Renter = _db.Renters.Find(id);
            if (renterVM.Renter == null)
            {
                return NotFound();
            }

            return View(renterVM);
        }
                                                                                                                                                                                                                                                                                                                                                                            

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(RenterVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Renters.Update(obj.Renter);
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
            var obj = _db.Renters.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? RenterId)
        {
            var obj = _db.Renters.Find(RenterId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Renters.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

