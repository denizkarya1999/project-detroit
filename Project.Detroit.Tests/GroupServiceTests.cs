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
    public class GroupServiceTests
    {
        [Fact]
        public async Task CanGetAllGroups()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var group1 = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "Group1",
                };

                var group2 = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "Group2",
                };

                await context.Groups.AddRangeAsync(group1, group2);
                await context.SaveChangesAsync();

                var groups = await groupService.getAllGroupsAsync();

                Assert.NotNull(groups);
                Assert.Equal(2, groups.Count);
            }
        }

        [Fact]
        public async Task CanGetGroupById()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var groupId = Guid.NewGuid();
                var group = new Group
                {
                    Id = groupId,
                    GroupName = "TestGroup",
                };

                await context.Groups.AddAsync(group);
                await context.SaveChangesAsync();

                var retrievedGroup = await groupService.getGroupByIdAsync(groupId);

                Assert.NotNull(retrievedGroup);
                Assert.Equal("TestGroup", retrievedGroup.GroupName);
            }
        }

        [Fact]
        public async Task CanGetGroupByName()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "TestGroup",
                };

                await context.Groups.AddAsync(group);
                await context.SaveChangesAsync();

                var retrievedGroup = await groupService.getGroupByNameAsync("TestGroup");

                Assert.NotNull(retrievedGroup);
                Assert.Equal("TestGroup", retrievedGroup.GroupName);
            }
        }

        [Fact]
        public async Task CanGetGroupByMember()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

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

                var group1 = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "Group1",
                };

                var group2 = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "Group2",
                };

                group2.Members.Add(James);

                await context.Groups.AddRangeAsync(group1, group2);
                await context.SaveChangesAsync();

                var retrievedGroup = await groupService.GetGroupByMemberAsync(James.Name);

                Assert.NotNull(retrievedGroup);
                Assert.Equal("Group2", retrievedGroup.GroupName);
            }
        }

        [Fact]
        public async Task CanCreateGroup()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var newGroup = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "NewGroup",
                };

                await groupService.createGroupAsync(newGroup);

                var createdGroup = await context.Groups.FindAsync(newGroup.Id);

                Assert.NotNull(createdGroup);
                Assert.Equal("NewGroup", createdGroup.GroupName);
            }
        }

        [Fact]
        public async Task CanUpdateGroup()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var groupToUpdate = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "GroupToUpdate",
                    groupBalance = 100,
                };

                await context.Groups.AddAsync(groupToUpdate);
                await context.SaveChangesAsync();

                groupToUpdate.groupBalance = 200;
                await groupService.UpdateGroupAsync(groupToUpdate);

                var updatedGroup = await context.Groups.FindAsync(groupToUpdate.Id);

                Assert.NotNull(updatedGroup);
                Assert.Equal(200, updatedGroup.groupBalance);
            }
        }

        [Fact]
        public async Task CanDeleteGroup()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var groupService = new GroupService(context);

                var groupToDelete = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "GroupToDelete",
                };

                await context.Groups.AddAsync(groupToDelete);
                await context.SaveChangesAsync();

                await groupService.DeleteGroupAsync(groupToDelete);

                var deletedGroup = await context.Groups.FindAsync(groupToDelete.Id);

                Assert.Null(deletedGroup);
            }
        }
    }
}