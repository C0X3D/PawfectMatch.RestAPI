using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.DataLayer
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public int UserId { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public DateTime? DateOfBirth { get; set; }   
    }
}
