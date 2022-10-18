using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class taskBoardIdNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "341acbbf-49c8-44f7-9269-95f8079a400d");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a828c675-f130-46ab-99e9-34d7ea76f91f", 0, "3acbd6ec-b6d6-49df-95a6-619662e39c89", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAELCTNFs4+Xl1D6ziTNI+qNUCbwvgtCdcdjCEVdp8fHA4axlMVa3AJ6to+drQt5Lr+g==", null, false, "09dc6ada-b22b-4200-9315-adf83f6c8a8c", false, "guest" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 9, 15, 17, 28, 53, 226, DateTimeKind.Local).AddTicks(2603), "a828c675-f130-46ab-99e9-34d7ea76f91f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 5, 15, 17, 28, 53, 226, DateTimeKind.Local).AddTicks(2656), "a828c675-f130-46ab-99e9-34d7ea76f91f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 10, 5, 17, 28, 53, 226, DateTimeKind.Local).AddTicks(2662), "a828c675-f130-46ab-99e9-34d7ea76f91f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 15, 17, 28, 53, 226, DateTimeKind.Local).AddTicks(2666), "a828c675-f130-46ab-99e9-34d7ea76f91f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a828c675-f130-46ab-99e9-34d7ea76f91f");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "341acbbf-49c8-44f7-9269-95f8079a400d", 0, "f381a0b6-19c3-4716-8d7c-7ef875c119fb", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEMivsXQ4w1zp+UVQCEyuIHTKJrtMHSmbwdmdWR88H7XA04eNrX+D2CvMvRLkCUitfg==", null, false, "1b394b46-c898-47c3-9887-a58e3edfb74e", false, "guest" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 9, 14, 14, 5, 54, 876, DateTimeKind.Local).AddTicks(7286), "341acbbf-49c8-44f7-9269-95f8079a400d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 5, 14, 14, 5, 54, 876, DateTimeKind.Local).AddTicks(7326), "341acbbf-49c8-44f7-9269-95f8079a400d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 10, 4, 14, 5, 54, 876, DateTimeKind.Local).AddTicks(7331), "341acbbf-49c8-44f7-9269-95f8079a400d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 14, 14, 5, 54, 876, DateTimeKind.Local).AddTicks(7334), "341acbbf-49c8-44f7-9269-95f8079a400d" });
        }
    }
}
