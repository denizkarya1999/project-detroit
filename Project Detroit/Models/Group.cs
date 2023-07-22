using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Detroit.Models
{
    //This is a model for group database
    public class Group
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string GroupName { get; set; }
        public IList<userAccount> Members { get; } = new List<userAccount>();
        public decimal? groupBalance { get; set; }
        public decimal? groupExpense { get; set; }
        public IList<Payment>? groupPayments { get; } = new List<Payment>();
        public IList<Expense>? groupApprovedExpenses { get; } = new List<Expense>();
        public IList<Notification>? groupNotifications { get; } = new List<Notification>();
        public IList<Report>? groupReports { get; } = new List<Report>();

        //Make connections between userAccount and shared
        public ICollection<shared> SharedItems { get; set; }
    }
}
