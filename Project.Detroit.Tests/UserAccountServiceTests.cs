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
        public async Task GetUserByVariousTypes()
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

                // Create James as user
                var James = new userAccount
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
                await userAccountService.addUserAsync(James);
                var getById = await userAccountService.getUserByIdAsync(James.Id);
                var getByName = await userAccountService.getUserByNameAsync(James.Name);
                var getByEmail = await userAccountService.getUserByEmailAsync(James.Email);

                // Assert
                Assert.Equal(James.Id, getById.Id);
                Assert.Equal(James.Name, getByName.Name);
                Assert.Equal(James.Email, getByEmail.Email);
            }
        }

        [Fact]
        public async Task CanGetAllUsers()
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

                // Create James as user
                var James = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "James",
                    Surname = "Springfield",
                    Email = "jspringfield@umich.edu",
                    Password = "J1399",
                    individualBalance = 180000.00m,
                    individualExpense = 700000m
                };

                //Create Brifanica as user
                var Brifanica = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "Brifanica",
                    Surname = "Cockfosters",
                    Email = "bockfosters@umich.edu",
                    Password = "BockFosters346",
                    individualBalance = 190000.00m,
                    individualExpense = 800000m
                };

                // Act
                await userAccountService.addUserAsync(James);
                await userAccountService.addUserAsync(Brifanica);
                var userAccounts = await userAccountService.getAllUsersAsync();

                // Assert
                for (int i = 0; i < userAccounts.Count; i++)
                {
                    if (userAccounts[i].Name == Brifanica.Name)
                    {
                        Assert.Equal(Brifanica.Name, userAccounts[i].Name);
                    }
                }
            }
        }

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
                await userAccountService.addUserAsync(testUser);
                var deleteUser = await userAccountService.DeleteUserAsync(testUser);

                //Assert
                Assert.True(deleteUser); //Check whether the service can delete user or not
            }
        }

        [Fact]
        public async Task CanUpdateUser()
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
                await userAccountService.addUserAsync(testUser);
                var James = await userAccountService.getUserByIdAsync(testUser.Id);
                James.Surname = "Acikbas";
                await userAccountService.UpdateUserAsync(James);
                James = await userAccountService.getUserByIdAsync(James.Id);

                //Assert
                Assert.Equal("Acikbas", James.Surname);
            }
        }
    }
}