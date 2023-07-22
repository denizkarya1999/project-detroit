using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class PaymentService
    {
        //Property
        private readonly AppDBContext _appDBContext;

        //Constructor
        public PaymentService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        //Get payments based on userId
        public async Task<List<Payment>> GetPaymentsBasedOnUserId(Guid userId)
        {
            return await _appDBContext.Payments
                .Where(payment => payment.UserAccountID == userId)
                .ToListAsync();
        }

        //Get payments based on groupId
        public async Task<List<Payment>> GetPaymentsBasedOnGroupId(Guid groupId)
        {
            return await _appDBContext.Payments
                .Where(payment => payment.GroupID == groupId)
                .ToListAsync();
        }

        // Read (Get payment by ID)
        public async Task<Payment?> GetPaymentById(Guid paymentId)
        {
            return await _appDBContext.Payments.FindAsync(paymentId);
        }

        // Create (Add new payment)
        public async Task<bool> CreatePayment(Payment payment)
        {
            _appDBContext.Payments.Add(payment);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Update (Edit existing payment)
        public async Task<bool> UpdatePayment(Payment payment)
        {
            _appDBContext.Payments.Update(payment);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Delete (Remove payment)
        public async Task<bool> DeletePayment(Guid paymentId)
        {
            var payment = await _appDBContext.Payments.FindAsync(paymentId);
            if (payment == null)
                return false;

            _appDBContext.Payments.Remove(payment);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}
