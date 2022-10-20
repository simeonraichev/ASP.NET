using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardFinal.Data.Migrations
{
    public partial class TasksSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c6577f8d-ec8f-4434-ac36-b203ce893a6c", 0, "adc27b66-abd4-4d1f-8857-a78c6361fc03", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEB2Eu+y8d4sjPh9vwd2jixzes8BheIxs3rA4225ZVgpHaIGVlvB1y71/Cj39qzE/dA==", null, false, "f553ea9c-7236-4408-b419-f3c1a094b81e", false, "guest" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 9, 20, 23, 13, 22, 677, DateTimeKind.Local).AddTicks(7029), "Learn using ASP.NET Core Identity", "c6577f8d-ec8f-4434-ac36-b203ce893a6c", "Prepare for ASP.NET Fundamentals exam" },
                    { 2, 3, new DateTime(2022, 5, 20, 23, 13, 22, 677, DateTimeKind.Local).AddTicks(7067), "Learn using EF Core and Ms SQL Server Managment Studio", "c6577f8d-ec8f-4434-ac36-b203ce893a6c", "Improve EF Core skills" },
                    { 3, 2, new DateTime(2022, 10, 10, 23, 13, 22, 677, DateTimeKind.Local).AddTicks(7070), "Learn using ASP.NET Core Identity", "c6577f8d-ec8f-4434-ac36-b203ce893a6c", "Improve ASP.NET Core skills" },
                    { 4, 3, new DateTime(2021, 10, 20, 23, 13, 22, 677, DateTimeKind.Local).AddTicks(7073), "Prepare by solving old Mid and Final exams", "c6577f8d-ec8f-4434-ac36-b203ce893a6c", "Prepare for C# Fundamentals Exam" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6577f8d-ec8f-4434-ac36-b203ce893a6c");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
