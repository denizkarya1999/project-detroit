using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    //This is a model for userAccount database
    public class userAccount 
    {
        [Key] [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required] [MinLength(8)]
        [MaxLength(255)]
        public string Password { get; set; }
        public decimal? individualBalance { get; set; }
        public decimal? individualExpense { get; set; }
        public IList<Payment>? individualPayments { get; } = new List<Payment>();
        public IList<Expense>? userApprovedExpenses { get; } = new List<Expense>();
        public IList<Notification>? userNotifications { get; } = new List<Notification>();
        public IList<Report>? userReports { get; } = new List<Report>();

        //Make connections between the group and user account
        public Guid? GroupID { get; set; }
        public Group? Group { get; set; }

        //Make connections between userAccount and shared
        public ICollection<shared> SharedItems { get; set; }
    }
}