using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public AppUser? AppUser { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string? Status { get; set; }
        public string? Comments { get; set; }
        public string? SessionId { get; set; } 
        public string? PaymentIntentId { get; set; } // il luam din stripe daca payment-ul va fi successfull
        [Required]
        public string Name { get; set; }
        [Display(Name ="Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }


    }
}
