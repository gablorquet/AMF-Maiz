using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMF.Core.Model
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }
    
    }

    public class Animateur : User
    {
        
    }

    public class Player : User
    {
        public List<Character> Characters { get; set; }

        public void UpdateFrom(Player data)
        {
            FirstName = data.FirstName;
            LastName = data.LastName;
            Username = data.Username;
            Email = data.Email;
        }
    }
}
