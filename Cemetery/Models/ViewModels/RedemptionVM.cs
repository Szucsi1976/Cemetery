using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models.ViewModels
{
    public class RedemptionVM
    {
        public Redemption Redemption { get; set; }
        public IEnumerable<SelectListItem> TypeDropDownGrave { get; set; }      //A sírok felsorolásához
        public IEnumerable<SelectListItem> TypeDropDownRenter{ get; set; }      //A sírmegváltók felsorolásához
        public IEnumerable<SelectListItem> TypeDropDownTender { get; set; }     //A sírgondozók felsorolásához

    }
}
