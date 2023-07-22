using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class NotificationService
    {
        private readonly AppDBContext _appDBContext;

        public NotificationService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        // Read (Get notification by ID)
        public async Task<Notification> GetNotificationById(Guid notificationId)
        {
            return await _appDBContext.Notifications.FindAsync(notificationId);
        }

        // Read (Get notifications based on user ID)
        public async Task<List<Notification>> GetNotificationsByUserId(Guid userId)
        {
            return await _appDBContext.Notifications
                .Where(notification => notification.UserAccountID == userId)
                .ToListAsync();
        }

        // Read (Get notifications based on group ID)
        public async Task<List<Notification>> GetNotificationsByGroupId(Guid groupId)
        {
            return await _appDBContext.Notifications
                .Where(notification => notification.GroupID == groupId)
                .ToListAsync();
        }

        // Create (Add new notification)
        public async Task<bool> CreateNotification(Notification notification)
        {
            _appDBContext.Notifications.Add(notification);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Update (Edit existing notification)
        public async Task<bool> UpdateNotification(Notification notification)
        {
            _appDBContext.Notifications.Update(notification);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Delete (Remove notification)
        public async Task<bool> DeleteNotification(Guid notificationId)
        {
            var notification = await _appDBContext.Notifications.FindAsync(notificationId);
            if (notification == null)
                return false;

            _appDBContext.Notifications.Remove(notification);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}