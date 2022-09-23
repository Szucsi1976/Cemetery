using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cemetery.Models;
using Cemetery.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cemetery.Utility;

namespace Cemetery.Controllers
{
    [Authorize(Roles = Helper.Admin)]
    public class DeadController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DeadController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Dead> objList = _db.Deads;
            foreach (var obj in objList)         // Ez kell a típusok megjelenítéséhez
            {
                obj.Religion = _db.Religions.FirstOrDefault(u => u.ReligionId == obj.DeadReligionId);
                obj.Grave= _db.Graves.FirstOrDefault(u => u.GraveId == obj.DeadGraveId);
                obj.Burial = _db.Burials.FirstOrDefault(u => u.FuneralId == obj.DeadBurialId);
                obj.Settlement = _db.Settlements.FirstOrDefault(u => u.SettlementId == obj.DeadBirthSettlementId);
            }
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            DeadVM deadVM = new DeadVM()
            {
                Dead= new Dead(),
                TypeDropDownGrave = _db.Graves.Select(i => new SelectListItem
                {
                    Text = i.GraveType + ". típus, " + i.Parcel+". parcella, " +i.Row+ ", sor " +i.Side+" oldal, "+i.Size+" méret",
                    Value = i.GraveId.ToString()
                }),

                 TypeDropDownBurial = _db.Burials.Select(i => new SelectListItem
                 {
                     Text = "temettető: "+i.FuneralName+ ", időpont: "+ i.FuneralDateTime,
                     Value = i.FuneralId.ToString()
                 }),

                  TypeDropDownSettlement = _db.Settlements.Select(i => new SelectListItem
                  {
                      Text = i.Station+" "+ i.PostalCode,
                      Value = i.SettlementId.ToString()
                  }),

                  TypeDropDownReligion = _db.Religions.Select(i => new SelectListItem
                  {
                      Text = i.ReligionName,
                      Value = i.ReligionId.ToString()
                  })
            };
            return View(deadVM);
        }
        
        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeadVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Deads.Add(obj.Dead);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
             DeadVM deadVM = new DeadVM()
            {
                Dead= new Dead(),
                TypeDropDownGrave = _db.Graves.Select(i => new SelectListItem
                {
                    Text = i.GraveType + " típus, " + i.Parcel+". parcella, " +i.Row+ ". sor, " +i.Side+" oldal, "+i.Size+" méret",
                    Value = i.GraveId.ToString()
                }),

                 TypeDropDownBurial = _db.Burials.Select(i => new SelectListItem
                 {
                     Text = "temettető: "+i.FuneralName+ ", időpont: "+ i.FuneralDateTime +", Temetkezési vállalat: " +i.Undertaker ,
                     Value = i.FuneralId.ToString()
                 }),

                  TypeDropDownSettlement = _db.Settlements.Select(i => new SelectListItem
                  {
                      Text = i.Station+" "+ i.PostalCode,
                      Value = i.SettlementId.ToString()
                  }),

                 TypeDropDownReligion = _db.Religions.Select(i => new SelectListItem
                 {
                     Text = i.ReligionName,
                     Value = i.ReligionId.ToString()
                 })

             };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            deadVM.Dead = _db.Deads.Find(id);
            if (deadVM.Dead == null)
            {
                return NotFound();
            }

            return View(deadVM);
        }
                                                                                                                                                                                                                                                                                                                                                                            

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(DeadVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Deads.Update(obj.Dead);
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
            var obj = _db.Deads.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Delete
        public IActionResult DeletePost(int? DeadId)
        {
            var obj = _db.Deads.Find(DeadId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Deads.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

