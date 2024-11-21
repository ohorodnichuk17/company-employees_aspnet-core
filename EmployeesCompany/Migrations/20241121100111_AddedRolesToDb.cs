using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesCompany.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62e20e0d-c44c-4865-a077-5fd73c2ecafd", "9b95012e-ec79-4aa3-98d7-cc4a97d97fab", "Manager", "MANAGER" },
                    { "674cb9c0-50c4-470b-b6e7-395cee07a441", "e865c47d-934c-4db7-b043-5a83f529dce0", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e20e0d-c44c-4865-a077-5fd73c2ecafd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "674cb9c0-50c4-470b-b6e7-395cee07a441");
        }
    }
}
