using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class UserAccountService
    {
        //Property
        private readonly AppDBContext _appDBContext;

        //Constructor
        public UserAccountService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        //Get all users
        public async Task<List<userAccount>> getAllUsersAsync()
        {
            return await _appDBContext.UserAccounts.ToListAsync();
        }

        //Get user by Id
        public async Task<userAccount?> getUserByIdAsync(Guid id)
        {
            userAccount? _user = await _appDBContext.UserAccounts.FirstOrDefaultAsync(c => c.Id.Equals(id));
            return _user;
        }

        //Get user by Name
        public async Task<userAccount?> getUserByNameAsync(String name)
        {
            userAccount? _user = await _appDBContext.UserAccounts.FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _user;
        }

        //Get user by Email
        public async Task<userAccount?> getUserByEmailAsync(string email)
        {
            try
            {
                userAccount? _user = await _appDBContext.UserAccounts.FirstOrDefaultAsync(c => c.Email.Equals(email));
                return _user;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred while retrieving user by email: {ex.Message}");

                // Optionally, rethrow the exception
                // throw;

                // If retrieval fails due to the exception
                return null;
            }
        }

        //Add user
        public async Task<bool> addUserAsync(userAccount _user)
        {
            await _appDBContext.UserAccounts.AddAsync(_user);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        //Update user
        public async Task<bool> UpdateUserAsync(userAccount _user)
        {
            _appDBContext.UserAccounts.Update(_user);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        //Delete user
        public async Task<bool> DeleteUserAsync(userAccount _user)
        {
            _appDBContext.UserAccounts.Remove(_user);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}
