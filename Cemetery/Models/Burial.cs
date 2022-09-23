using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public enum burialMethod { urna, koporsó }
    public class Burial                      //Temetések, lehet lényegében teljesen üres objektum
    {
        [Key]
        [DisplayName("Azonosító")]
        public int FuneralId { get; set; }
        [DisplayName("Temettető neve")]
        [MaxLength(100)]
        public string FuneralName { get; set; }   //lehet jogi vagy magánszemély is a temettető

        [DisplayName("Temetés dátuma")]
        public DateTime FuneralDateTime { get; set; } //Nem biztos, hogy tudjuk

        [DisplayName("Temetkezési vállalat")]
        public int BurialUndertakerId { get; set; }   //Ezt sem

        [DisplayName("Temetési mód")]
        public burialMethod DeathBurialMethod { get; set; }


        [ForeignKey("BurialUndertakerId")]
        public virtual Undertaker Undertaker { get; set; }   //Ez a csatolt temetkezési vállalat tábla

    }
}
