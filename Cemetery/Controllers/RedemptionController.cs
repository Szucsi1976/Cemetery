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

namespace RCemetery.Controllers
{
    [Authorize(Roles = Helper.Admin)]
    public class RedemptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RedemptionController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Redemption> objList = _db.Redemptions;
            foreach (var obj in objList)         // Ez kell a típusok megjelenítéséhez
            {
                obj.Grave= _db.Graves.FirstOrDefault(u => u.GraveId == obj.RedemptionGraveId);
                obj.Renter = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionRenterId);
                obj.Tender = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionTenderId);
            }
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            RedemptionVM redemptionVM = new RedemptionVM()
            {
                Redemption = new Redemption(),
                TypeDropDownGrave = _db.Graves.Select(i => new SelectListItem
                {
                    Text = i.GraveType + " típus, " + i.Parcel+" parcella," +i.Row+ ", sor" +i.Side+" oldal, "+i.Size+" méret",
                    Value = i.GraveId.ToString()
                }),

                 TypeDropDownRenter = _db.Renters.Select(i => new SelectListItem
                 {
                     Text = i.RenterLastname+" "+ i.RenterFirstname +" telefonszám: " +i.RenterPhoneNumber ,
                     Value = i.RenterId.ToString()
                 }),

                  TypeDropDownTender = _db.Renters.Select(i => new SelectListItem
                  {
                      Text = i.RenterLastname+" " + i.RenterFirstname + " telefonszám: " + i.RenterPhoneNumber,
                      Value = i.RenterId.ToString()
                  })
            };
            return View(redemptionVM);
        }
        
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RedemptionVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Redemptions.Add(obj.Redemption);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            RedemptionVM redemptionVM = new RedemptionVM()
            {
                Redemption = new Redemption(),
                TypeDropDownGrave = _db.Graves.Select(i => new SelectListItem
                {
                    Text = i.GraveType + " típus, " + i.Parcel + " parcella," + i.Row + ", sor" + i.Side + " oldal, " + i.Size + " méret",
                    Value = i.GraveId.ToString()
                }),

                TypeDropDownRenter = _db.Renters.Select(i => new SelectListItem
                {
                    Text = i.RenterLastname + " " + i.RenterFirstname + " telefonszám: " + i.RenterPhoneNumber,
                    Value = i.RenterId.ToString()
                }),

                TypeDropDownTender = _db.Renters.Select(i => new SelectListItem
                {
                    Text = i.RenterLastname + " " + i.RenterFirstname + " telefonszám: " + i.RenterPhoneNumber,
                    Value = i.RenterId.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            redemptionVM.Redemption = _db.Redemptions.Find(id);
            if (redemptionVM.Redemption == null)
            {
                return NotFound();
            }

            return View(redemptionVM);
        }
                                                                                                                                                                                                                                                                                                                                                                            

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(RedemptionVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Redemptions.Update(obj.Redemption);
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
            var obj = _db.Redemptions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? RedemptionId)
        {
            var obj = _db.Redemptions.Find(RedemptionId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Redemptions.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

