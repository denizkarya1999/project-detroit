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
    public class PaymentServiceTests
    {
        [Fact]
        public async Task CanCreatePayment()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var paymentService = new PaymentService(context);

                var newPayment = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "NewPayment",
                    PaymentAmount = 100.00m,
                };

                await paymentService.CreatePayment(newPayment);

                var createdPayment = await paymentService.GetPaymentById(newPayment.Id);

                Assert.NotNull(createdPayment);
                Assert.Equal("NewPayment", createdPayment.PaymentName);
                Assert.Equal(100.00m, createdPayment.PaymentAmount);
            }
        }

        [Fact]
        public async Task CanUpdatePayment()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var paymentService = new PaymentService(context);

                var paymentToUpdate = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "PaymentToUpdate",
                    PaymentAmount = 200.00m,
                };

                await paymentService.CreatePayment(paymentToUpdate);

                paymentToUpdate.PaymentName = "UpdatedPayment";
                paymentToUpdate.PaymentAmount = 300.00m;
                await paymentService.UpdatePayment(paymentToUpdate);

                var updatedPayment = await paymentService.GetPaymentById(paymentToUpdate.Id);

                Assert.NotNull(updatedPayment);
                Assert.Equal("UpdatedPayment", updatedPayment.PaymentName);
                Assert.Equal(300.00m, updatedPayment.PaymentAmount);
            }
        }

        [Fact]
        public async Task CanDeletePayment()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var paymentService = new PaymentService(context);

                var paymentToDelete = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "PaymentToDelete",
                };

                await paymentService.CreatePayment(paymentToDelete);
                await paymentService.DeletePayment(paymentToDelete.Id);

                var deletedPayment = await paymentService.GetPaymentById(paymentToDelete.Id);

                Assert.Null(deletedPayment);
            }
        }

        [Fact]
        public async Task CanGetPaymentsBasedOnUserId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var paymentService = new PaymentService(context);

                var user = new userAccount
                {
                    Id = Guid.NewGuid(),
                    Name = "UserWithPayments",
                };

                var payment1 = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "Payment1",
                    UserAccountID = user.Id,
                };

                var payment2 = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "Payment2",
                    UserAccountID = user.Id,
                };

                await paymentService.CreatePayment(payment1);
                await paymentService.CreatePayment(payment2);

                var payments = await paymentService.GetPaymentsBasedOnUserId(user.Id);

                Assert.NotNull(payments);
                Assert.Equal(2, payments.Count);
                Assert.True(payments.All(payment => payment.UserAccountID == user.Id));
            }
        }

        [Fact]
        public async Task CanGetPaymentsBasedOnGroupId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var paymentService = new PaymentService(context);

                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = "GroupWithPayments",
                };

                var payment1 = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "Payment1",
                    GroupID = group.Id,
                };

                var payment2 = new Payment
                {
                    Id = Guid.NewGuid(),
                    PaymentName = "Payment2",
                    GroupID = group.Id,
                };

                await paymentService.CreatePayment(payment1);
                await paymentService.CreatePayment(payment2);

                var payments = await paymentService.GetPaymentsBasedOnGroupId(group.Id);

                Assert.NotNull(payments);
                Assert.Equal(2, payments.Count);
                Assert.True(payments.All(payment => payment.GroupID == group.Id));
            }
        }
    }
}