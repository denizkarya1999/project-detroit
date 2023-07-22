using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class SharedService
    {
        //Property
        private readonly AppDBContext _appDBContext;

        //Constructor
        public SharedService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        //Get shared by UserId
        public async Task<List<shared>> GetSharedItemsByUserId(Guid userId)
        {
            //Get the sharedItems and save to a variable
            List<shared> sharedItems = await _appDBContext.Shared
                .Where(s => s.UserId == userId)
                .ToListAsync();
            //Return the shared items
            return sharedItems;
        }

        // Add shared item from both UserAccount and Group
        public async Task<bool> AddSharedAsync(userAccount user, Group group)
        {
            var sharedId = Guid.NewGuid(); // Create a new sharedId
            shared newSharedItem = new shared()
            {
                SharedId = sharedId,
                sharedBalance = group.groupBalance + user.individualBalance,
                sharedExpense = group.groupExpense + user.individualExpense,
                UserId = user.Id,
                GID = group.Id
            };
            await _appDBContext.Shared.AddAsync(newSharedItem);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Update shared item
        public async Task<bool> UpdateSharedAsync(userAccount user, Group group)
        {
            shared? existingSharedItem = await _appDBContext.Shared
                .FirstOrDefaultAsync(s => s.UserId == user.Id && s.GID == group.Id);

            if (existingSharedItem != null)
            {
                existingSharedItem.sharedBalance = group.groupBalance + user.individualBalance;
                existingSharedItem.sharedExpense = group.groupExpense + user.individualExpense;

                _appDBContext.Shared.Update(existingSharedItem);
                _appDBContext.UserAccounts.Update(user);
                _appDBContext.Groups.Update(group);
                await _appDBContext.SaveChangesAsync();
                return true;
            }

            return false; // Return false if the shared item does not exist
        }

        //Delete shared object
        public async Task<bool> deleteSharedAsync(userAccount user, Group group)
        {
            shared? existingSharedItem = await _appDBContext.Shared
                .FirstOrDefaultAsync(s => s.UserId == user.Id && s.GID == group.Id);

            if (existingSharedItem != null)
            {
                _appDBContext.Shared.Remove(existingSharedItem);
                _appDBContext.UserAccounts.Remove(user);
                _appDBContext.Groups.Remove(group);
                await _appDBContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
