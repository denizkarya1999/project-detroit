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
    public class ReportServiceTests
    {
        [Fact]
        public async Task CanGetReportById()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var reportId = Guid.NewGuid();
                var report = new Report
                {
                    Id = reportId,
                    ReportName = "TestReport",
                };

                await context.Reports.AddAsync(report);
                await context.SaveChangesAsync();

                var retrievedReport = await reportService.GetReportById(reportId);

                Assert.NotNull(retrievedReport);
                Assert.Equal("TestReport", retrievedReport.ReportName);
            }
        }

        [Fact]
        public async Task CanGetReportsByUserId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var userId = Guid.NewGuid();
                var report1 = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "Report1",
                    UserAccountID = userId,
                };

                var report2 = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "Report2",
                    UserAccountID = userId,
                };

                await context.Reports.AddRangeAsync(report1, report2);
                await context.SaveChangesAsync();

                var reports = await reportService.GetReportsByUserId(userId);

                Assert.NotNull(reports);
                Assert.Equal(2, reports.Count);
                Assert.True(reports.All(report => report.UserAccountID == userId));
            }
        }

        [Fact]
        public async Task CanGetReportsByGroupId()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var groupId = Guid.NewGuid();
                var report1 = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "Report1",
                    GroupID = groupId,
                };

                var report2 = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "Report2",
                    GroupID = groupId,
                };

                await context.Reports.AddRangeAsync(report1, report2);
                await context.SaveChangesAsync();

                var reports = await reportService.GetReportsByGroupId(groupId);

                Assert.NotNull(reports);
                Assert.Equal(2, reports.Count);
                Assert.True(reports.All(report => report.GroupID == groupId));
            }
        }

        [Fact]
        public async Task CanCreateReport()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var newReport = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "NewReport",
                    Details = "Report details",
                };

                await reportService.CreateReport(newReport);

                var createdReport = await context.Reports.FindAsync(newReport.Id);

                Assert.NotNull(createdReport);
                Assert.Equal("NewReport", createdReport.ReportName);
                Assert.Equal("Report details", createdReport.Details);
            }
        }

        [Fact]
        public async Task CanUpdateReport()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var reportToUpdate = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "ReportToUpdate",
                    Details = "Initial details",
                };

                await context.Reports.AddAsync(reportToUpdate);
                await context.SaveChangesAsync();

                reportToUpdate.Details = "Updated details";
                await reportService.UpdateReport(reportToUpdate);

                var updatedReport = await context.Reports.FindAsync(reportToUpdate.Id);

                Assert.NotNull(updatedReport);
                Assert.Equal("Updated details", updatedReport.Details);
            }
        }

        [Fact]
        public async Task CanDeleteReport()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDBContext(options))
            {
                var reportService = new ReportService(context);

                var reportToDelete = new Report
                {
                    Id = Guid.NewGuid(),
                    ReportName = "ReportToDelete",
                };

                await context.Reports.AddAsync(reportToDelete);
                await context.SaveChangesAsync();

                await reportService.DeleteReport(reportToDelete.Id);

                var deletedReport = await context.Reports.FindAsync(reportToDelete.Id);

                Assert.Null(deletedReport);
            }
        }
    }
}