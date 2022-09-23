using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public enum graveType { urna, kripta, koporsó, kolumbárium }   
    public enum size {urnafal, egyszemélyes, dupla, családi } 
    public enum side { jobb, bal}
    public class Grave                              //Sírok 
    {
        [Key]
        [DisplayName("Azonosító")]
        public int GraveId { get; set; }

        [Required]
        [DisplayName("Típus")]
        public graveType GraveType { get; set; }

        [Required]
        [DisplayName("Méret")]
        public size Size { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [DisplayName("Sor")]
        public int Row { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        [DisplayName("Parcella")]
        public int Parcel { get; set; }

        [Required]
        [DisplayName("Oldal")]
        public side Side {get;set;}
    }
}
