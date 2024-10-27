using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface INotificationDal
    {
        Task<List<Notification>?> Get(int userId, string userRole, int pageIndex);
        Task<bool> UpdateMarked(Notification? notification);
        Task<bool> UpdateRead(int notificationId);
    }
}
