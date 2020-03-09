using Microsoft.AspNetCore.Mvc;
using SafewalkApplication.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SafewalkApplication.Models
{
    public partial class User : IPerson
    {
        public string Id { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string HomeAddress { get; set; }
        public string Interest { get; set; }
        public string Token { get; set; }
    }
}
