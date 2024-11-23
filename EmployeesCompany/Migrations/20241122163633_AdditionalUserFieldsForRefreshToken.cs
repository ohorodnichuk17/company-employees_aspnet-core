using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesCompany.Migrations
{
    public partial class AdditionalUserFieldsForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e20e0d-c44c-4865-a077-5fd73c2ecafd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "674cb9c0-50c4-470b-b6e7-395cee07a441");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c28d7f3e-0837-483d-8075-d359b6b757dd", "5958f199-8cab-4f38-837d-6e9cae88644a", "Manager", "MANAGER" },
                    { "fd1a0ad6-e4b8-486c-9f47-dd7f43b297f7", "ece4bc3c-a26d-406e-9c23-d458a538c4f8", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c28d7f3e-0837-483d-8075-d359b6b757dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd1a0ad6-e4b8-486c-9f47-dd7f43b297f7");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62e20e0d-c44c-4865-a077-5fd73c2ecafd", "9b95012e-ec79-4aa3-98d7-cc4a97d97fab", "Manager", "MANAGER" },
                    { "674cb9c0-50c4-470b-b6e7-395cee07a441", "e865c47d-934c-4db7-b043-5a83f529dce0", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
