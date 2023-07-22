using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class GroupService
    {
        //Property
        private readonly AppDBContext _appDBContext;

        //Constructor
        public GroupService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        //Get all groups
        public async Task<List<Group>> getAllGroupsAsync()
        {
            return await _appDBContext.Groups.ToListAsync();
        }

        //Get group by Id
        public async Task<Group?> getGroupByIdAsync(Guid id)
        {
            Group? _group = await _appDBContext.Groups.FirstOrDefaultAsync(c => c.Id.Equals(id));
            return _group;
        }

        //Get group by Name
        public async Task<Group?> getGroupByNameAsync(String name)
        {
            Group? _group = await _appDBContext.Groups.FirstOrDefaultAsync(c => c.GroupName.Equals(name));
            return _group;
        }

        //Get group by members
        public async Task<Group?> GetGroupByMemberAsync(string memberName)
        {
            Group? member = await _appDBContext.Groups
                .FirstOrDefaultAsync(g => g.Members.Any(m => m.Name.Equals(memberName)));

            return member;
        }

        //Create group
        public async Task<bool> createGroupAsync(Group _group)
        {
            await _appDBContext.Groups.AddAsync(_group);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        //Add new member to the group
        public async Task<bool> addMemberAsync(string groupName, userAccount _member)
        {
            //Search for member
            Group? group = await _appDBContext.Groups.FirstOrDefaultAsync(g => g.GroupName.Equals(groupName));

            //If the group is not null add new member
            if (group != null)
            {
                group.Members.Add(_member);
                await _appDBContext.SaveChangesAsync();
            }
            return true;
        }

        //Remove member from the group
        public async Task<bool> removeMemberAsync(string groupName, userAccount _member)
        {
            //Search for member
            Group? group = await _appDBContext.Groups.FirstOrDefaultAsync(g => g.GroupName.Equals(groupName));

            //If the group is not null add new member
            if (group != null)
            {
                group.Members.Remove(_member);
                await _appDBContext.SaveChangesAsync();
            }
            return true;
        }

        //Update group
        public async Task<bool> UpdateGroupAsync(Group _group)
        {
            _appDBContext.Groups.Update(_group);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        //Leave group
        public async Task LeaveGroupAsync(Guid memberId)
        {
            //Get the member based on ID
            userAccount? member = await _appDBContext.UserAccounts.FindAsync(memberId);

            if (member != null)
            {
                member.GroupID = null; // Reset the GroupId of the member
                await _appDBContext.SaveChangesAsync();
            }
        }

        //Delete group
        public async Task<bool> DeleteGroupAsync(Group _group)
        {
            _appDBContext.Groups.Remove(_group);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}
