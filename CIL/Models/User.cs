using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class User : IdentityUser<Guid>
    {
        //By default: Id, Email, PasswordHash, PhoneNumber, UserName
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Subscription Subscription {  get; set; }
        public DateTime? TimeSubscriptionPaid { get; set; }
        public bool? IsSubscriptionPaid { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Combination> Combinations { get; set; }
    }
}
