using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfileImage { get; set; }
        public Guid Subscription { get; set; }
    }
}
