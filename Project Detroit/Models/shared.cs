using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    public class shared
    {
        [Key]
        public Guid SharedId { get; set; }
        public decimal? sharedBalance { get; set; }
        public decimal? sharedExpense { get; set; }
        public IList<Payment>? sharedPayments { get; } = new List<Payment>();
        public IList<Notification>? sharedNotifications { get; } = new List<Notification>();
        public IList<Report>? sharedReports { get; } = new List<Report>();

        //Connect objects from shared and group tables
        public Guid UserId { get; set; }
        public userAccount user { get; set; }

        public Guid GID { get; set; }
        public Group group { get; set; }
    }
}
