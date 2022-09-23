using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models.ViewModels
{
    public class DeadVM
    {
        public Dead Dead { get; set; }
        public IEnumerable<SelectListItem> TypeDropDownReligion { get; set; }      //A vallásdok felsorolásához
        public IEnumerable<SelectListItem> TypeDropDownGrave { get; set; }      //A sírok felsorolásához
        public IEnumerable<SelectListItem> TypeDropDownBurial { get; set; }      //A temetések felsorolásához
        public IEnumerable<SelectListItem> TypeDropDownSettlement { get; set; }     //A települések felsorolásához
    }
}
