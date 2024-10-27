using DonationAppDemo.Models;

namespace DonationAppDemo.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>?> Get(int pageIndex);
        Task<bool> UpdateRead(int notificationId);
    }
}
