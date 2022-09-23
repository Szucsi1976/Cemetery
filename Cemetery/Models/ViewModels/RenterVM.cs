using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models.ViewModels
{
    public class RenterVM
    {
        public Renter Renter { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
