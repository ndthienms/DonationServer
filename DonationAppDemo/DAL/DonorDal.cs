using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class DonorDal : IDonorDal
    {
        private readonly DonationDbContext _context;

        public DonorDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<Donor?> GetByPhoneNum(string phoneNum)
        {
            var userInformation = await _context.Donor.Where(x => x.AccountId == phoneNum).FirstOrDefaultAsync();
            return userInformation;
        }
        public async Task<Donor> Add(DonorDto donorDto)
        {
            var donor = new Donor()
            {
                Name = donorDto.Name,
                Gender = donorDto.Gender,
                Dob = donorDto.Dob,
                Email = donorDto.Email,
                Address = donorDto.Address,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                UpdatedDate = DateTime.Now,
                UpdatedBy = null,
                AccountId = donorDto.PhoneNum,
            };
            _context.Donor.Add(donor);
            await _context.SaveChangesAsync();
            return donor;
        }
    }
}
