using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public class Religion
    {
        [Key]
        public int ReligionId { get; set; }

        [Required]
        [DisplayName("Vallás")]
        [MaxLength(100)]
        public string ReligionName { get; set; }
    }
}
