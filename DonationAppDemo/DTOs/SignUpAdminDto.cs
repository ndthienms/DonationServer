﻿namespace DonationAppDemo.DTOs
{
    public class SignUpAdminDto
    {
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
    }
}
