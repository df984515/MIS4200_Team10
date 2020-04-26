using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS4200_Team10.Models
{
    public class Recognition
    {
        [Key]
        public int recognitionID { get; set; }

                
        [Display(Name = "Employee")]
        [Required(ErrorMessage ="Please choose an employee to recognize")]
        public Guid ID { get; set; }
        public virtual UserDetails UserDetails { get; set; }

        [Display(Name = "Recognition Date")]
        [Required(ErrorMessage = "Please enter date employee exhibited core value")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime recognitionDate { get; set; }

        [Display(Name = "Core Value")]
        [Required(ErrorMessage = "Please choose a core value")]
        public int valueID { get; set; }
        public virtual Values Values { get; set; }

        [Display(Name = "Recognition Reason")]
        [Required(ErrorMessage = "Please describe why you are recongizing this employee")]
        [StringLength(140)]
        public string recognitionReason { get; set; }

        [Display(Name = "Employee")]
        public string employee { get { return UserDetails.firstName + " " + UserDetails.lastName; } }
        [Display(Name = "Core Value")]
        public string coreValue { get { return Values.coreValue; } }
    }
}