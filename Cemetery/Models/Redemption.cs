using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public class Redemption     //megváltások
    {
        [Key]
        [DisplayName("Megváltásazonosító")]
        public int RedemptionId { get; set; }

        [DisplayName("Megváltott sírhely")]
        [Required]
        public int RedemptionGraveId { get; set; }

        [DisplayName("Megváltó személy")]
        [Required]
        public int RedemptionRenterId { get; set; }
        
        [DisplayName("Sírgondozó személy")]
        [Required]
        public int RedemptionTenderId { get; set; }

        [DisplayName("Sírmegváltás időpontja")]
        [Required]
        public DateTime RedemptionDate { get; set; }

        [DisplayName("Sírmegváltás időtartama (év)")]
        [Required]
        [Range(1, int.MaxValue)]
        public int RedemptionPeriod { get; set; }


        [ForeignKey("RedemptionGraveId")]
        public virtual Grave Grave { get; set; }   //Ez a csatolt tábla sír tábla

        [ForeignKey("RedemptionRenterId")]
        public virtual Renter Renter { get; set; }   //Ez a csatolt tábla sírmegváltó tábla

        [ForeignKey("RedemptionTenderId")]
        public virtual Renter Tender { get; set; }   //Ez a csatolt tábla sírgondozó tábla megegyezik a sírmegváltóval
    }
}
