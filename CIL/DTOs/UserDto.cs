using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
