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
    public class ExpenseServiceTests
    {
        [Fact]
        public async Task CanCreateExpense()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var expenseService = new ExpenseService(context);

                var newExpense = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "NewExpense",
                    ExpenseAmount = 100.00m,
                };

                await expenseService.CreateExpense(newExpense);

                var createdExpense = await expenseService.GetExpenseById(newExpense.Id);

                Assert.NotNull(createdExpense);
                Assert.Equal("NewExpense", createdExpense.ExpenseName);
                Assert.Equal(100.00m, createdExpense.ExpenseAmount);
            }
        }

        [Fact]
        public async Task CanUpdateExpense()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var expenseService = new ExpenseService(context);

                var expenseToUpdate = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "ExpenseToUpdate",
                    ExpenseAmount = 200.00m,
                };

                await expenseService.CreateExpense(expenseToUpdate);

                expenseToUpdate.ExpenseName = "UpdatedExpense";
                expenseToUpdate.ExpenseAmount = 300.00m;
                await expenseService.UpdateExpense(expenseToUpdate);

                var updatedExpense = await expenseService.GetExpenseById(expenseToUpdate.Id);

                Assert.NotNull(updatedExpense);
                Assert.Equal("UpdatedExpense", updatedExpense.ExpenseName);
                Assert.Equal(300.00m, updatedExpense.ExpenseAmount);
            }
        }

        [Fact]
        public async Task CanDeleteExpense()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var expenseService = new ExpenseService(context);

                var expenseToDelete = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "ExpenseToDelete",
                };

                await expenseService.CreateExpense(expenseToDelete);
                await expenseService.DeleteExpense(expenseToDelete.Id);

                var deletedExpense = await expenseService.GetExpenseById(expenseToDelete.Id);

                Assert.Null(deletedExpense);
            }
        }

        [Fact]
        public async Task CanGetExpensesByUserId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var expenseService = new ExpenseService(context);

                var user = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "UserWithExpenses",
                };

                var expense1 = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "Expense1",
                    UserAccountID = user.Id,
                };

                var expense2 = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "Expense2",
                    UserAccountID = user.Id,
                };

                await expenseService.CreateExpense(expense1);
                await expenseService.CreateExpense(expense2);

                var expenses = await expenseService.GetExpensesByUserId(user.Id);

                Assert.NotNull(expenses);
                Assert.Equal(2, expenses.Count);
                Assert.True(expenses.All(expense => expense.UserAccountID == user.Id));
            }
        }

        [Fact]
        public async Task CanGetExpensesByGroupId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var expenseService = new ExpenseService(context);

                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "GroupWithExpenses",
                };

                var expense1 = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "Expense1",
                    GroupID = group.Id,
                };

                var expense2 = new Expense
                {
                    Id = Guid.NewGuid(),
                    ExpenseName = "Expense2",
                    GroupID = group.Id,
                };

                await expenseService.CreateExpense(expense1);
                await expenseService.CreateExpense(expense2);

                var expenses = await expenseService.GetExpensesByGroupId(group.Id);

                Assert.NotNull(expenses);
                Assert.Equal(2, expenses.Count);
                Assert.True(expenses.All(expense => expense.GroupID == group.Id));
            }
        }
    }
}