using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface IDonationDal
    {
        Task<Donation> Add(PaymentResponseDto responseDto);
    }
}
