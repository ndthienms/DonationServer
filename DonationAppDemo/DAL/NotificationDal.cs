using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class NotificationDal : INotificationDal
    {
        private readonly DonationDbContext _context;

        public NotificationDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Notification>?> Get(int userId, string userRole, int pageIndex)
        {
            if (pageIndex == 1)
            {
                var notifications = await _context.Notification
                .Where(x => x.ToUserId == userId && x.ToUserRole == userRole)
                .OrderByDescending(x => x.NotificationDate)
                .Skip((pageIndex - 1) * 20)
                .Take(20)
                .ToListAsync();
                return notifications;
            }
            else // pageIndex > 1
            {
                var notifications = await _context.Notification
                .Where(x => x.ToUserId == userId && x.ToUserRole == userRole &&
                x.NotificationDate <=
                _context.Notification
                    .Where(z => z.ToUserId == userId && z.ToUserRole == userRole && z.Marked == true)
                    .Max(z => z.NotificationDate))
                .OrderByDescending(x => x.NotificationDate)
                .Skip((pageIndex - 1) * 20)
                .Take(20)
                .ToListAsync();
                return notifications;
            }
        }
        public async Task<bool> UpdateMarked(Notification? notification)
        {
            if(notification == null)
            {
                return false;
            }

            notification.Marked = true;

            _context.Notification.Update(notification);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateRead(int notificationId)
        {
            var notification = await _context.Notification.Where(x => x.Id == notificationId).FirstOrDefaultAsync();

            if(notification == null)
            {
                return false;
            }

            notification.IsRead = true;

            _context.Notification.Update(notification);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
