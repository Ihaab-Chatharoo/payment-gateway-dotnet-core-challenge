using System;
using System.Collections.Generic;

namespace Checkout.Repository.DB
{
    public partial class TblUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
