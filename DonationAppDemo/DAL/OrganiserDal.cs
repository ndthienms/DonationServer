using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Helper;
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
        public async Task<List<Organiser>> GetAll(int pageIndex)
        {
            var usersInformation = await _context.Organiser
                .Skip((pageIndex - 1) * 20)
                .Take(20)
                .ToListAsync();
            return usersInformation;
        }
        public async Task<List<Organiser>> GetSearchedList(int pageIndex, string text)
        {
            string? nomalizedText = StringExtension.NormalizeString(text);
            var usersInformation = await _context.Organiser
                .Where(x => x.AccountId == nomalizedText || x.Id.ToString() == nomalizedText || StringExtension.NormalizeString(x.Name) == nomalizedText)
                .Skip((pageIndex - 1) * 20)
                .Take(20)
                .ToListAsync();
            return usersInformation;
        }
        public async Task<List<Organiser>> GetAllUnCensored(int pageIndex)
        {
            var usersInformation = await _context.Organiser
                .Where(x => x.AcceptedBy == null)
                .Skip((pageIndex - 1) * 20)
                .Take(20)
                .ToListAsync();
            return usersInformation;
        }
        public async Task<Organiser?> GetById(int id)
        {
            var userInformation = await _context.Organiser.Where(x => x.Id == id).FirstOrDefaultAsync();
            return userInformation;
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
                AcceptedBy = null,
                AcceptedDate = null,
                AccountId = organiserDto.PhoneNum,
            };
            _context.Organiser.Add(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }
        public async Task<Organiser> Update(int organiserId, OrganiserDto organiserDto)
        {
            var organiser = await _context.Organiser.Where(x => x.Id == organiserId).FirstOrDefaultAsync();
            if (organiser == null)
            {
                throw new Exception($"Not found user id {organiserId}");
            }

            organiser.Name = organiserDto.Name;
            organiser.Gender = organiserDto.Gender;
            organiser.Dob = organiserDto.Dob;
            organiser.Email = organiserDto.Email;
            organiser.Address = organiserDto.Address;
            organiser.Description = organiserDto.Description;
            organiser.UpdatedDate = DateTime.Now;
            organiser.UpdatedBy = organiserId;


            _context.Organiser.Update(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }
        public async Task<Organiser> UpdateApprovement(int organiserId, int adminId)
        {
            var organiser = await _context.Organiser.Where(x => x.Id == organiserId).FirstOrDefaultAsync();
            if (organiser == null)
            {
                throw new Exception($"Not found user id {organiserId}");
            }

            organiser.AcceptedDate = DateTime.Now;
            organiser.AcceptedBy = adminId;

            _context.Organiser.Update(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }
        public async Task<Organiser> UpdateAva(int organiserId, string avaSrc, string avaSrcPublicId)
        {
            var organiser = await _context.Organiser.Where(x => x.Id == organiserId).FirstOrDefaultAsync();
            if (organiser == null)
            {
                throw new Exception($"Not found user id {organiserId}");
            }

            organiser.AvaSrc = avaSrc;
            organiser.AvaSrcPublicId = avaSrcPublicId;
            organiser.UpdatedDate = DateTime.Now;
            organiser.UpdatedBy = organiserId;


            _context.Organiser.Update(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }

        public async Task<Organiser> UpdateCertification(int organiserId, string certificationSrc, string certificationSrcPublicId)
        {
            var organiser = await _context.Organiser.Where(x => x.Id == organiserId).FirstOrDefaultAsync();
            if (organiser == null)
            {
                throw new Exception($"Not found user id {organiserId}");
            }

            organiser.CertificationSrc = certificationSrc;
            organiser.CertificationSrcPublicId = certificationSrcPublicId;
            organiser.UpdatedDate = DateTime.Now;
            organiser.UpdatedBy = organiserId;


            _context.Organiser.Update(organiser);
            await _context.SaveChangesAsync();
            return organiser;
        }

        public async Task<bool> Delete(int organiserId)
        {
            var organiser = await _context.Organiser.Where(x => x.Id == organiserId).FirstOrDefaultAsync();
            if (organiser == null)
            {
                throw new Exception($"Not found user id {organiserId}");
            }
            _context.Organiser.Remove(organiser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
