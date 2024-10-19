﻿using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface IDonorDal
    {
        Task<List<Donor>> GetAll(int pageIndex);
        Task<List<Donor>> GetSearchedList(int pageIndex, string text);
        Task<Donor?> GetById(int id);
        Task<Donor?> GetByPhoneNum(string phoneNum);
        Task<Donor> Add(DonorDto donorDto);
        Task<Donor> Update(int donorId, DonorDto donorDto);
        Task<Donor> UpdateAva(int donorId, string avaSrc, string avaSrcPublicId);
    }
}
