using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class OrganiserDal : IOrganiserDal
    {
        private readonly DonationDbContext _context;

        public OrganiserDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<Organiser?> GetByPhoneNum(string phoneNum)
        {
            var userInformation = await _context.Organiser.Where(x => x.AccountId == phoneNum).FirstOrDefaultAsync();
            return userInformation;
        }
        public async Task<Organiser> Add(OrganiserDto organiserDto, string? certificationPublicId)
        {
            var organiser = new Organiser()
            {
                Name = organiserDto.Name,
                Gender = organiserDto.Gender,
                Dob = organiserDto.Dob,
                Email = organiserDto.Email,
                Address = organiserDto.Address,
                CertificationSrc = organiserDto.CertificationSrc,
                CertificationSrcPublicId = certificationPublicId,
                Description = organiserDto.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                UpdatedDate = DateTime.Now,
                UpdatedBy = null,
                AccountId = organiserDto.PhoneNum,
            };
            _context.Organiser.Add(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }
    }
}
