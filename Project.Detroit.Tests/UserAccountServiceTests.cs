using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Project.Detroit.Models;
using Project.Detroit.Services;
using Xunit;

namespace Project.Detroit.Tests
{
    public class UserAccountServiceTests
    {
        [Fact]
        public async Task CanCreateNewUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name for each test
                .Options;

            // Create a real instance of the AppDBContext with the in-memory database options
            using (var context = new AppDBContext(options))
            {
                // Create an instance of the UserAccountService with the real context
                var userAccountService = new UserAccountService(context);

                // Create a test user
                var testUser = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "James",
                    Surname = "Springfield",
                    Email = "jspringfield@umich.edu",
                    Password = "J1399",
                    individualBalance = 180000.00m,
                    individualExpense = 700000m
                };

                // Act
                var result = await userAccountService.addUserAsync(testUser);

                // Assert
                Assert.True(result); // The addUserAsync method should return true on success
            }
        }

        [Fact]
        public async Task CanDeleteUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name for each test
                .Options;
            // Create a real instance of the AppDBContext with the in-memory database options
            using (var context = new AppDBContext(options))
            {
                // Create an instance of the UserAccountService with the real context
                var userAccountService = new UserAccountService(context);

                // Create a test user
                var testUser = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "James",
                    Surname = "Springfield",
                    Email = "jspringfield@umich.edu",
                    Password = "J1399",
                    individualBalance = 180000.00m,
                    individualExpense = 700000m
                };

                // Act
                var addUser = await userAccountService.addUserAsync(testUser);
                var deleteUser = await userAccountService.DeleteUserAsync(testUser);

                //Assert
                Assert.True(deleteUser); //Check whether the service can delete user or not
            }
        }
    }
}