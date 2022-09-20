using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Utility
{
    public static class Helper
    {
        public static string Admin = "Adminisztrátor";
        public static string Curious = "Érdeklődő";
        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{Value = Helper.Admin, Text=Helper.Admin},
                new SelectListItem{Value = Helper.Curious, Text=Helper.Curious}
            };
        }
    }
}
