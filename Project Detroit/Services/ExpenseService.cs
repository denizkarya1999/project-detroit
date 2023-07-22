using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class ExpenseService
    {
        private readonly AppDBContext _appDBContext;

        public ExpenseService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        // Read (Get expense by ID)
        public async Task<Expense> GetExpenseById(Guid expenseId)
        {
            return await _appDBContext.Expenses.FindAsync(expenseId);
        }

        // Read (Get expenses based on user ID)
        public async Task<List<Expense>> GetExpensesByUserId(Guid userId)
        {
            return await _appDBContext.Expenses
                .Where(expense => expense.UserAccountID == userId)
                .ToListAsync();
        }

        // Read (Get expenses based on group ID)
        public async Task<List<Expense>> GetExpensesByGroupId(Guid groupId)
        {
            return await _appDBContext.Expenses
                .Where(expense => expense.GroupID == groupId)
                .ToListAsync();
        }

        // Create (Add new expense)
        public async Task<bool> CreateExpense(Expense expense)
        {
            _appDBContext.Expenses.Add(expense);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Update (Edit existing expense)
        public async Task<bool> UpdateExpense(Expense expense)
        {
            _appDBContext.Expenses.Update(expense);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Delete (Remove expense)
        public async Task<bool> DeleteExpense(Guid expenseId)
        {
            var expense = await _appDBContext.Expenses.FindAsync(expenseId);
            if (expense == null)
                return false;

            _appDBContext.Expenses.Remove(expense);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}