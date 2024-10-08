﻿using DonationAppDemo.DTOs;
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
        public async Task<List<Donor>> GetAll()
        {
            var usersInformation = await _context.Donor.ToListAsync();
            return usersInformation;
        }
        public async Task<Donor?> GetById(int id)
        {
            var userInformation = await _context.Donor.Where(x => x.Id == id).FirstOrDefaultAsync();
            return userInformation;
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
        public async Task<Donor> Update(int donorId, DonorDto donorDto)
        {
            var donor = await _context.Donor.Where(x => x.Id == donorId).FirstOrDefaultAsync();
            if (donor == null)
            {
                throw new Exception($"Not found user id {donorId}");
            }

            donor.Name = donorDto.Name;
            donor.Gender = donorDto.Gender;
            donor.Dob = donorDto.Dob;
            donor.Email = donorDto.Email;
            donor.Address = donorDto.Address;
            donor.UpdatedDate = DateTime.Now;
            donor.UpdatedBy = donorId;

            _context.Donor.Update(donor);
            await _context.SaveChangesAsync();
            return donor;
        }
        public async Task<Donor> UpdateAva(int donorId, string avaSrc, string avaSrcPublicId)
        {
            var donor = await _context.Donor.Where(x => x.Id == donorId).FirstOrDefaultAsync();
            if (donor == null)
            {
                throw new Exception($"Not found user id {donorId}");
            }

            donor.AvaSrc = avaSrc;
            donor.AvaSrcPublicId = avaSrcPublicId;
            donor.UpdatedDate = DateTime.Now;
            donor.UpdatedBy = donorId;


            _context.Donor.Update(donor);
            await _context.SaveChangesAsync();
            return donor;
        }
    }
}