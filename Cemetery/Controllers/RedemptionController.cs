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
    
    public class RedemptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RedemptionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = Helper.Admin)]
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

        [Authorize(Roles = Helper.Curious)]
        public IActionResult GetRedemptionsFromEmail(string email)   //Ez kell az e-mail alapján történő megváltásfelsoroláshoz
        {
            IEnumerable<Redemption> objList = _db.Redemptions;
            foreach (var obj in objList)         // Ez kell a típusok megjelenítéséhez
            {
                obj.Grave = _db.Graves.FirstOrDefault(u => u.GraveId == obj.RedemptionGraveId);
                obj.Renter = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionRenterId);
                obj.Tender = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionTenderId);
            }

            var objectumList= objList.ToList();
            objectumList.RemoveAll(x => x.Renter.RenterEmail != email);
            return View(objectumList);    
        }


        private List<Redemption> SearchLastRedemption(List<Redemption> objectumList)
        {
            for (int i = 0; i < objectumList.Count() - 1; i++)          //Megkeresem az utolsó megváltást
            {
                for (int j = i + 1; j < objectumList.Count(); j++)
                {
                    if (objectumList[i].Grave.GraveId == objectumList[j].Grave.GraveId)
                    {
                        if (objectumList[i].RedemptionDate.AddYears(objectumList[i].RedemptionPeriod) < (objectumList[j].RedemptionDate.AddYears(objectumList[j].RedemptionPeriod)))
                        {
                            objectumList[i] = objectumList[j];
                        }
                        objectumList.RemoveAt(j);
                        j--;
                    }
                }
            }
            return objectumList;
        }

        [Authorize(Roles = Helper.Admin)]
        public IActionResult GetLastRedemptions()
        { 
            IEnumerable<Redemption> objList = _db.Redemptions;
            foreach (var obj in objList)        
            {
                obj.Grave = _db.Graves.FirstOrDefault(u => u.GraveId == obj.RedemptionGraveId);
                obj.Renter = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionRenterId);
                obj.Tender = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionTenderId);
            }
            var objectumList = objList.ToList();        
            return View(SearchLastRedemption(objectumList));
        }


        [Authorize(Roles = Helper.Admin)]
        public IActionResult GetExpiredRedemptions()
        {
            IEnumerable<Redemption> objList = _db.Redemptions;
            foreach (var obj in objList)         
            {
                obj.Grave = _db.Graves.FirstOrDefault(u => u.GraveId == obj.RedemptionGraveId);
                obj.Renter = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionRenterId);
                obj.Tender = _db.Renters.FirstOrDefault(u => u.RenterId == obj.RedemptionTenderId);
            }
            var objectumList = objList.ToList();
            objectumList=SearchLastRedemption(objectumList);
            objectumList.RemoveAll(x => x.RedemptionDate.AddYears(x.RedemptionPeriod) > DateTime.Now);
            return View(objectumList);
        }

        //GET-CREATE
        [Authorize(Roles = Helper.Admin)]
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
        [Authorize(Roles = Helper.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RedemptionVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Redemptions.Add(obj.Redemption);
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

        [Authorize(Roles = Helper.Admin)]
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

        [Authorize(Roles = Helper.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Edit
        public IActionResult Edit(RedemptionVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Redemptions.Update(obj.Redemption);
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
        [Authorize(Roles = Helper.Admin)]
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
            var obj = _db.Redemptions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = Helper.Admin)]
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
            try
            {
                _db.Redemptions.Remove(obj);
                _db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = RedemptionId, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}

