using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        //Create datasets
        public DbSet<userAccount> UserAccounts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<shared> Shared { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }

        //Make connections and seed the data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Associate Group with userAccount 
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Members)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupID)
                .IsRequired(false);

            //Associate Payment with userAccount
            modelBuilder.Entity<userAccount>()
                .HasMany(e => e.individualPayments)
                .WithOne(e => e.userAccount)
                .HasForeignKey(e => e.UserAccountID)
                .IsRequired(false);

            //Associate Payment with Group
            modelBuilder.Entity<Group>()
                .HasMany(e => e.groupPayments)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupID)
                .IsRequired(false);

            //Associate Expense with userAccount
            modelBuilder.Entity<userAccount>()
                .HasMany(e => e.userApprovedExpenses)
                .WithOne(e => e.userAccount)
                .HasForeignKey(e => e.UserAccountID)
                .IsRequired(false);

            //Associate Expense with Group
            modelBuilder.Entity<Group>()
                .HasMany(e => e.groupApprovedExpenses)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupID)
                .IsRequired(false);

            //Associate Notification with userAccount
            modelBuilder.Entity<userAccount>()
                .HasMany(e => e.userNotifications)
                .WithOne(e => e.userAccount)
                .HasForeignKey(e => e.UserAccountID)
                .IsRequired(false);

            //Associate Notification with Group
            modelBuilder.Entity<Group>()
                .HasMany(e => e.groupNotifications)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupID)
                .IsRequired(false);

            //Associate Report with userAccount
            modelBuilder.Entity<userAccount>()
                .HasMany(e => e.userReports)
                .WithOne(e => e.userAccount)
                .HasForeignKey(e => e.UserAccountID)
                .IsRequired(false);

            //Associate Report with Group
            modelBuilder.Entity<Group>()
                .HasMany(e => e.groupReports)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupID)
                .IsRequired(false);

            // Associate UserAccount with Shared
            modelBuilder.Entity<shared>()
                .HasOne(s => s.user)
                .WithMany(u => u.SharedItems)
                .HasForeignKey(s => s.UserId);

            // Associate Group with Shared
            modelBuilder.Entity<shared>()
                .HasOne(s => s.group)
                .WithMany(g => g.SharedItems)
                .HasForeignKey(s => s.GID);

            //Generate a new Id
            var groupId = Guid.NewGuid();
            var denizId = Guid.NewGuid();
            var parId = Guid.NewGuid();

            //Add new sample group
            Group turing = new Group()
            {
                Id = groupId,
                GroupName = "Group Turing",
                groupBalance = 100000000,
                groupExpense = 350000
            };

            //Add new sample user
            userAccount deniz = new userAccount()
            {
                Id = denizId,
                Name = "Deniz",
                Surname = "Acikbas",
                Email = "dacikbas@umich.edu",
                Password = "Karya99DA",
                individualBalance = 822450.50m,
                individualExpense = 400000m,
                GroupID = turing.Id
            };

            //Add new sample user
            userAccount Par = new userAccount()
            {
                Id = parId,
                Name = "Parashar",
                Surname = "Parikh",
                Email = "parikhpc@umich.edu",
                Password = "Par1234",
                individualBalance = 1022450.50m,
                individualExpense = 900000m,
                GroupID = turing.Id
            };

            //Add groups and userAccounts into shared
            shared denizShared = new shared()
            {
                SharedId = Guid.NewGuid(),
                sharedBalance = turing.groupBalance + deniz.individualBalance,
                sharedExpense = turing.groupExpense + deniz.individualExpense,
                UserId = deniz.Id,
                GID = turing.Id
            };

            shared parShared = new shared()
            {
                SharedId = Guid.NewGuid(),
                sharedBalance = turing.groupBalance + Par.individualBalance,
                sharedExpense = turing.groupExpense + Par.individualExpense,
                UserId = Par.Id,
                GID = turing.Id
            };

            //Generate a group payment (hotel stay)
            var hotelGroupStay = new Payment()
            {
                Id = Guid.NewGuid(),
                PaymentName = "Two Night Stay at London Sheraton",
                PaymentAmount = 3000,
                GroupID = groupId
            };

            //Add entity into payment class
            modelBuilder.Entity<Payment>()
                .HasData(hotelGroupStay);

            //Generate an individual payment (UK visa application)
            var ukVISA = new Payment()
            {
                Id = Guid.NewGuid(),
                PaymentName = "UK Tourist Visa",
                PaymentAmount = 130,
                UserAccountID = denizId
            };

            //Add entity into payment class
            modelBuilder.Entity<Payment>()
                .HasData(ukVISA);

            //Generate an expense for group (Server Crash)
            var ServerCrash = new Expense()
            {
                Id = Guid.NewGuid(),
                ExpenseName = "Local Server Crashed",
                ExpenseAmount = 13000,
                GroupID = groupId
            };

            //Add entity into expense class
            modelBuilder.Entity<Expense>()
                .HasData(ServerCrash);

            //Generate an expense for user (OS Installation
            var OSInstallation = new Expense()
            {
                Id = Guid.NewGuid(),
                ExpenseName = "Install Windows 11",
                ExpenseAmount = 100,
                UserAccountID = denizId
            };

            //Add entity into expense class
            modelBuilder.Entity<Expense>()
                .HasData(OSInstallation);

            //Generate a notification for group (Irrelevant Content)
            var ContentPolicy = new Notification()
            {
                Id = Guid.NewGuid(),
                NotificationName = "Irrelevant Content",
                Details = "The expense posted here is irrelevant with the purpose of this website.",
                GroupID = groupId
            };

            //Add entity into notification class
            modelBuilder.Entity<Notification>()
                .HasData(ContentPolicy);

            //Generate a notification for user (Low Balance)
            var LowBalance = new Notification()
            {
                Id = Guid.NewGuid(),
                NotificationName = "Low Balance",
                Details = "Your balance is low. Please update your balance and add your expense.",
                UserAccountID = denizId
            };

            //Add entity into notification class
            modelBuilder.Entity<Notification>()
                .HasData(LowBalance);

            //Generate an example report for group
            Report report = new Report()
            {
                Id = Guid.NewGuid(),
                ReportName = "Expense Analysis Report",
                Details = "This report analyzes the expenses for the previous quarter. It covers various categories, including travel, office supplies, and utilities. " +
              "The report provides a breakdown of the total amount spent in each category and identifies any notable trends or patterns. " +
              "Additionally, it highlights areas where cost-saving measures can be implemented. " +
              "The report aims to provide insights for better expense management and decision-making.",
                GroupID = turing.Id
            };

            //Add entity into report class
            modelBuilder.Entity<Report>()
                .HasData(report);

            //Generate an example report for user
            Report userReport = new Report()
            {
                Id = Guid.NewGuid(),
                ReportName = "Monthly Expense Report",
                Details = "This report provides a detailed breakdown of your monthly expenses. +" +
              "It includes categories such as rent, groceries, transportation, and entertainment. +" +
              "The report showcases your spending patterns and highlights areas where you can potentially save money. +" +
              "Use this report to track your expenses, set budgets, and make informed financial decisions.",
                UserAccountID = denizId,
            };

            //Add entity into report class
            modelBuilder.Entity<Report>()
                .HasData(userReport);

            //Add entities into groups, userAccount and shared
            modelBuilder.Entity<Group>()
                .HasData(turing);
            modelBuilder.Entity<userAccount>()
                .HasData(deniz);
            modelBuilder.Entity<userAccount>()
                .HasData(Par);
            modelBuilder.Entity<shared>()
                .HasData(denizShared, parShared);

        }
    }
}