using System;
using System.Collections.Generic;

namespace SafewalkApplication.Models
{
    public partial class User
    {
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string HomeAddress { get; set; }
        public string Interest { get; set; }
        public string Token { get; set; }
    }
}
