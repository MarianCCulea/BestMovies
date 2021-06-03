using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestMovies.Model.Domain
{
    public class Userache
    {
        public Userache()
        {
        }

        public Userache(string email, string passWord)
        {
            this.email = email;
            this.password = passWord;

        }

        public string email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Minimum 6 characters")]
        public string password { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Userache user &&
                   email == user.email &&
                   password == user.password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(email, password);
        }
    }
}
