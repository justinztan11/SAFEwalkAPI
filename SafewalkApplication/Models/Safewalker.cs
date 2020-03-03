using System;
using System.Collections.Generic;

namespace SafewalkApplication.Models
{
    public partial class Safewalker
    {
        public string WalkerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
    }
}
