using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team10.Models
{
    public class Values
    {
        [Key]
        public int valueID { get; set; }

        [Display(Name ="Core Value")]
        [Required]
        public string coreValue { get; set; }

        public ICollection<Recognition> Recognition { get; set; }
    }
}