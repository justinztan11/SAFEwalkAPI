using SafewalkApplication.Contracts;
using System;
using System.Collections.Generic;

namespace SafewalkApplication.Models
{
    public partial class Safewalker : IPerson
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public decimal? CurrLat { get; set; }
        public decimal? CurrLng { get; set; }
        public string Token { get; set; }
    }
}
