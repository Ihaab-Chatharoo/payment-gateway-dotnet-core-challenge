using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout.Infrastructure
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid Card Number")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Please enter a valid Card Number")]
        public string CardNumber { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid CVV")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Minimum 3 numbers required")]
        public string Cvv { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Amount { get; set; }
    }
}
