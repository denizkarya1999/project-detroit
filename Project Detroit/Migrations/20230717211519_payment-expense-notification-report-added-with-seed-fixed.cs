using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Detroit.Migrations
{
    /// <inheritdoc />
    public partial class paymentexpensenotificationreportaddedwithseedfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    groupBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    groupExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    individualBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    individualExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payment_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Report_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shared",
                columns: table => new
                {
                    SharedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sharedBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sharedExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shared", x => x.SharedId);
                    table.ForeignKey(
                        name: "FK_Shared_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shared_Groups_GID",
                        column: x => x.GID,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shared_UserAccounts_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificationshared",
                columns: table => new
                {
                    SharedItemsSharedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sharedNotificationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificationshared", x => new { x.SharedItemsSharedId, x.sharedNotificationsId });
                    table.ForeignKey(
                        name: "FK_Notificationshared_Notification_sharedNotificationsId",
                        column: x => x.sharedNotificationsId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificationshared_Shared_SharedItemsSharedId",
                        column: x => x.SharedItemsSharedId,
                        principalTable: "Shared",
                        principalColumn: "SharedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paymentshared",
                columns: table => new
                {
                    SharedItemsSharedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sharedPaymentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymentshared", x => new { x.SharedItemsSharedId, x.sharedPaymentsId });
                    table.ForeignKey(
                        name: "FK_Paymentshared_Payment_sharedPaymentsId",
                        column: x => x.sharedPaymentsId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paymentshared_Shared_SharedItemsSharedId",
                        column: x => x.SharedItemsSharedId,
                        principalTable: "Shared",
                        principalColumn: "SharedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportshared",
                columns: table => new
                {
                    SharedItemsSharedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sharedReportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportshared", x => new { x.SharedItemsSharedId, x.sharedReportsId });
                    table.ForeignKey(
                        name: "FK_Reportshared_Report_sharedReportsId",
                        column: x => x.sharedReportsId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportshared_Shared_SharedItemsSharedId",
                        column: x => x.SharedItemsSharedId,
                        principalTable: "Shared",
                        principalColumn: "SharedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "GroupName", "groupBalance", "groupExpense" },
                values: new object[] { new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), "Group Turing", 100000000m, 350000m });

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "ExpenseAmount", "ExpenseName", "GroupID", "UserAccountID" },
                values: new object[] { new Guid("f5b20655-6d62-4fda-ac1d-740f2c629b2d"), 13000m, "Local Server Crashed", new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), null });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "Details", "GroupID", "NotificationName", "UserAccountID" },
                values: new object[] { new Guid("1918abb6-d56b-4278-a776-36448ac598da"), "The expense posted here is irrelevant with the purpose of this website.", new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), "Irrelevant Content", null });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "GroupID", "PaymentAmount", "PaymentName", "UserAccountID" },
                values: new object[] { new Guid("d23e58f9-b227-44b5-a6c5-4cd43c51ffbd"), new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), 3000m, "Two Night Stay at London Sheraton", null });

            migrationBuilder.InsertData(
                table: "Report",
                columns: new[] { "Id", "Details", "GroupID", "ReportName", "UserAccountID" },
                values: new object[] { new Guid("6a716f33-3481-4917-a5db-9c30cf0a8f90"), "This report analyzes the expenses for the previous quarter. It covers various categories, including travel, office supplies, and utilities. +The report provides a breakdown of the total amount spent in each category and identifies any notable trends or patterns. +Additionally, it highlights areas where cost-saving measures can be implemented. +The report aims to provide insights for better expense management and decision-making.", new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), "Expense Analysis Report", null });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "Email", "GroupID", "Name", "Password", "Surname", "individualBalance", "individualExpense" },
                values: new object[,]
                {
                    { new Guid("4df465f1-279f-4d55-a62f-23df9a04b792"), "parikhpc@umich.edu", new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), "Parashar", "Par1234", "Parikh", 1022450.50m, 900000m },
                    { new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434"), "dacikbas@umich.edu", new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), "Deniz", "Karya99DA", "Acikbas", 822450.50m, 400000m }
                });

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "ExpenseAmount", "ExpenseName", "GroupID", "UserAccountID" },
                values: new object[] { new Guid("9bcf85a4-0765-42ab-9744-df162d6e6ef8"), 100m, "Install Windows 11", null, new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434") });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "Details", "GroupID", "NotificationName", "UserAccountID" },
                values: new object[] { new Guid("e353e4c8-05b8-403b-b517-41d5d986c165"), "Your balance is low. Please update your balance and add your expense.", null, "Low Balance", new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434") });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "GroupID", "PaymentAmount", "PaymentName", "UserAccountID" },
                values: new object[] { new Guid("a3ff2285-b66e-4719-b241-3b003cd4e730"), null, 130m, "UK Tourist Visa", new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434") });

            migrationBuilder.InsertData(
                table: "Report",
                columns: new[] { "Id", "Details", "GroupID", "ReportName", "UserAccountID" },
                values: new object[] { new Guid("9d068e6b-4095-4a2b-8787-b617ec74846c"), "This report provides a detailed breakdown of your monthly expenses. +It includes categories such as rent, groceries, transportation, and entertainment. +The report showcases your spending patterns and highlights areas where you can potentially save money. +Use this report to track your expenses, set budgets, and make informed financial decisions.", null, "Monthly Expense Report", new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434") });

            migrationBuilder.InsertData(
                table: "Shared",
                columns: new[] { "SharedId", "ExpenseId", "GID", "UserId", "sharedBalance", "sharedExpense" },
                values: new object[,]
                {
                    { new Guid("38648722-df51-4a07-94c6-11b52626cbe3"), null, new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), new Guid("4df465f1-279f-4d55-a62f-23df9a04b792"), 101022450.50m, 1250000m },
                    { new Guid("694fe8c3-188a-4757-9ef2-f02c265e1bbf"), null, new Guid("de6d05ee-aafe-4997-b7cf-3c946b66b676"), new Guid("87cd9c66-bb1e-48be-a16f-d6faf0f6e434"), 100822450.50m, 750000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_GroupID",
                table: "Expense",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserAccountID",
                table: "Expense",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_GroupID",
                table: "Notification",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserAccountID",
                table: "Notification",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificationshared_sharedNotificationsId",
                table: "Notificationshared",
                column: "sharedNotificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_GroupID",
                table: "Payment",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserAccountID",
                table: "Payment",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentshared_sharedPaymentsId",
                table: "Paymentshared",
                column: "sharedPaymentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_GroupID",
                table: "Report",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserAccountID",
                table: "Report",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportshared_sharedReportsId",
                table: "Reportshared",
                column: "sharedReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Shared_ExpenseId",
                table: "Shared",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shared_GID",
                table: "Shared",
                column: "GID");

            migrationBuilder.CreateIndex(
                name: "IX_Shared_UserId",
                table: "Shared",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_GroupID",
                table: "UserAccounts",
                column: "GroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificationshared");

            migrationBuilder.DropTable(
                name: "Paymentshared");

            migrationBuilder.DropTable(
                name: "Reportshared");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Shared");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
