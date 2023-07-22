using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Detroit.Models;

namespace Project.Detroit.Services
{
    public class ReportService
    {
        private readonly AppDBContext _appDBContext;

        public ReportService(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        // Read (Get report by ID)
        public async Task<Report> GetReportById(Guid reportId)
        {
            return await _appDBContext.Reports.FindAsync(reportId);
        }

        // Read (Get reports based on user ID)
        public async Task<List<Report>> GetReportsByUserId(Guid userId)
        {
            return await _appDBContext.Reports
                .Where(report => report.UserAccountID == userId)
                .ToListAsync();
        }

        // Read (Get reports based on group ID)
        public async Task<List<Report>> GetReportsByGroupId(Guid groupId)
        {
            return await _appDBContext.Reports
                .Where(report => report.GroupID == groupId)
                .ToListAsync();
        }

        // Create (Add new report)
        public async Task<bool> CreateReport(Report report)
        {
            _appDBContext.Reports.Add(report);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Update (Edit existing report)
        public async Task<bool> UpdateReport(Report report)
        {
            _appDBContext.Reports.Update(report);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Delete (Remove report)
        public async Task<bool> DeleteReport(Guid reportId)
        {
            var report = await _appDBContext.Reports.FindAsync(reportId);
            if (report == null)
                return false;

            _appDBContext.Reports.Remove(report);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
    }
}