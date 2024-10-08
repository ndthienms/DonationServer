using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class AdminDal : IAdminDal
    {
        private readonly DonationDbContext _context;

        public AdminDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Admin>> GetAll()
        {
            var usersInformation = await _context.Admin.ToListAsync();
            return usersInformation;
        }
        public async Task<Admin?> GetById(int id)
        {
            var userInformation = await _context.Admin.Where(x => x.Id == id).FirstOrDefaultAsync();
            return userInformation;
        }
        public async Task<Admin?> GetByPhoneNum(string phoneNum)
        {
            var userInformation = await _context.Admin.Where(x => x.AccountId == phoneNum).FirstOrDefaultAsync();
            return userInformation;
        }
        public async Task<Admin> Add(AdminDto adminDto)
        {
            var admin = new Admin()
            {
                Name = adminDto.Name,
                Gender = adminDto.Gender,
                Dob = adminDto.Dob,
                Email = adminDto.Email,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                UpdatedDate = DateTime.Now,
                UpdatedBy = null,
                AccountId = adminDto.PhoneNum,
            };
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task<Admin> Update(int adminId, AdminDto adminDto)
        {
            var admin = await _context.Admin.Where(x => x.Id == adminId).FirstOrDefaultAsync();
            if (admin == null)
            {
                throw new Exception($"Not found user id {adminId}");
            }

            admin.Name = adminDto.Name;
            admin.Gender = adminDto.Gender;
            admin.Dob = adminDto.Dob;
            admin.Email = adminDto.Email;
            admin.UpdatedDate = DateTime.Now;
            admin.UpdatedBy = adminId;

            _context.Admin.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
