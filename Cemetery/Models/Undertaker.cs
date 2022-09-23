using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public class Undertaker
    {
        [Key]
        public int UndertakerId { get; set; }

        [Required]
        [DisplayName("Temetkezési vállalat")]
        [MaxLength(100)]
        public string UndertakerName { get; set; }
    }
}
