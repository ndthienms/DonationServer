﻿using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IOrganiserDal
    {
        Task<List<Organiser>> GetAll();
        Task<Organiser?> GetById(int id);
        Task<Organiser?> GetByPhoneNum(string phoneNum);
        Task<Organiser> Add(OrganiserDto organiserDto, string? certificationPublicId);
        Task<Organiser> Update(int organiserId, OrganiserDto organiserDto);
        Task<Organiser> UpdateApprovement(int organiserId, int adminId);
        Task<Organiser> UpdateAva(int organiserId, string avaSrc, string avaSrcPublicId);
        Task<Organiser> UpdateCertification(int organiserId, string certificationSrc, string certificationSrcPublicId);
    }
}