using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Project.Detroit.Models;
using Project.Detroit.Services;
using Xunit;

namespace Project.Detroit.Tests
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task CanCreateNotification()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var notificationService = new NotificationService(context);

                var newNotification = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "NewNotification",
                    Details = "Notification details",
                };

                await notificationService.CreateNotification(newNotification);

                var createdNotification = await notificationService.GetNotificationById(newNotification.Id);

                Assert.NotNull(createdNotification);
                Assert.Equal("NewNotification", createdNotification.NotificationName);
                Assert.Equal("Notification details", createdNotification.Details);
            }
        }

        [Fact]
        public async Task CanUpdateNotification()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var notificationService = new NotificationService(context);

                var notificationToUpdate = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "NotificationToUpdate",
                    Details = "Initial details",
                };

                await notificationService.CreateNotification(notificationToUpdate);

                notificationToUpdate.Details = "Updated details";
                await notificationService.UpdateNotification(notificationToUpdate);

                var updatedNotification = await notificationService.GetNotificationById(notificationToUpdate.Id);

                Assert.NotNull(updatedNotification);
                Assert.Equal("Updated details", updatedNotification.Details);
            }
        }

        [Fact]
        public async Task CanDeleteNotification()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var notificationService = new NotificationService(context);

                var notificationToDelete = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "NotificationToDelete",
                };

                await notificationService.CreateNotification(notificationToDelete);
                await notificationService.DeleteNotification(notificationToDelete.Id);

                var deletedNotification = await notificationService.GetNotificationById(notificationToDelete.Id);

                Assert.Null(deletedNotification);
            }
        }

        [Fact]
        public async Task CanGetNotificationsBasedOnUserId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var notificationService = new NotificationService(context);

                var user = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "UserWithNotifications",
                };

                var notification1 = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "Notification1",
                    UserAccountID = user.Id,
                };

                var notification2 = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "Notification2",
                    UserAccountID = user.Id,
                };

                await notificationService.CreateNotification(notification1);
                await notificationService.CreateNotification(notification2);

                var notifications = await notificationService.GetNotificationsByUserId(user.Id);

                Assert.NotNull(notifications);
                Assert.Equal(2, notifications.Count);
                Assert.True(notifications.All(notification => notification.UserAccountID == user.Id));
            }
        }

        [Fact]
        public async Task CanGetNotificationsBasedOnGroupId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var notificationService = new NotificationService(context);

                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "GroupWithNotifications",
                };

                var notification1 = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "Notification1",
                    GroupID = group.Id,
                };

                var notification2 = new Notification
                {
                    Id = Guid.NewGuid(),
                    NotificationName = "Notification2",
                    GroupID = group.Id,
                };

                await notificationService.CreateNotification(notification1);
                await notificationService.CreateNotification(notification2);

                var notifications = await notificationService.GetNotificationsByGroupId(group.Id);

                Assert.NotNull(notifications);
                Assert.Equal(2, notifications.Count);
                Assert.True(notifications.All(notification => notification.GroupID == group.Id));
            }
        }
    }
}