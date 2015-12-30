using System;
using System.ComponentModel.DataAnnotations;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public PlayerViewModel(){}


        public PlayerViewModel(Player data)
        {
            Id = data.Id;
            FirstName = data.FirstName;
            LastName = data.LastName;
            DateOfBirth = data.DateOfBirth;
            Email = data.Email;
            Username = data.Username;

        }

        public Player AsPlayer()
        {
            return new Player
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                Username = this.Username
            };
        }
    }
}
