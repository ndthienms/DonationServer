using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IGeocodingService
    {
        Task<List<Locations>> GetLocationsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
