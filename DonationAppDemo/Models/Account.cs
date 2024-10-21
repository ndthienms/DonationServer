using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationAppDemo.Models
{
    public class Account
    {
        [Key]
        public string PhoneNum { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } //admin, donor, organiser
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set;}
        public bool? Disabled { get; set; } //if account signed up organiser role -> disabled == true (waiting for acceptance)
        [NotMapped]
        public ICollection<Admin>? Admins { get; set; }
        [NotMapped]
        public ICollection<Organiser>? Organisers { get; set; }
        [NotMapped]
        public ICollection<Donor>? Donors { get; set; }
    }
}
