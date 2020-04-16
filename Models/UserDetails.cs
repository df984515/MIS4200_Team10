using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS4200_Team10.Models
{
    public class UserDetails
    {
        [Required]
        
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Please include your email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please include your first name.")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Please include your last name.")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Please include your phone number.")]
        [Display(Name = "Primary Phone")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Office")]
        public string Office { get; set; }            
        

        [Required(ErrorMessage = "Please include your current position title.")]
        [Display(Name = "Current position")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Please include your initial hire date.")]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }        

        [Display(Name ="Full Name")]
        public string fullName { get { return firstName + ", " + lastName; } }

        public ICollection<Recognition> Recognition { get; set; }
    }
}