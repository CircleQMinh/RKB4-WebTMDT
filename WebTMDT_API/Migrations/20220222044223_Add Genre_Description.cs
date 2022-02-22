using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTMDT.Migrations
{
    public partial class AddGenre_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0e94c83e-be78-46ec-ab13-c907a5c7f72c");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5afb8d94-7964-4019-ba3e-2e9edf90f6a2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c907e276-4a36-4c04-a255-9e8ffa6f449a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e0963012-613e-43ee-9f01-8e63ef1c5568");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65adea28-540b-4ea8-8b8c-c2986d49ff70", "6767172f-1795-44bf-aacc-c5ecb9c911dc", "Shipper", "SHIPPER" },
                    { "918a0866-8565-431f-a2e7-daa2000e690e", "f6250092-e27c-4e72-9b91-4d09c3f7d1fd", "User", "USER" },
                    { "a51b62e7-01f4-427c-ba4e-3529d2ceb068", "1659578d-c014-40f8-a346-c9cfd970f24c", "Administrator", "ADMINISTRATOR" },
                    { "d1dc57c8-9469-4688-8544-981c492eef0b", "23eb30f7-7141-4f45-a4d7-e2026aa7dc37", "Employee", "EMPLOYEE" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "65adea28-540b-4ea8-8b8c-c2986d49ff70");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "918a0866-8565-431f-a2e7-daa2000e690e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a51b62e7-01f4-427c-ba4e-3529d2ceb068");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d1dc57c8-9469-4688-8544-981c492eef0b");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e94c83e-be78-46ec-ab13-c907a5c7f72c", "846c6707-59d7-4998-8058-d6d7002e773c", "Administrator", "ADMINISTRATOR" },
                    { "5afb8d94-7964-4019-ba3e-2e9edf90f6a2", "ace329e7-e9df-43f9-a010-9ff1c38d75e7", "Employee", "EMPLOYEE" },
                    { "c907e276-4a36-4c04-a255-9e8ffa6f449a", "4f5a7c21-c883-4d08-8506-ee45f4899c67", "User", "USER" },
                    { "e0963012-613e-43ee-9f01-8e63ef1c5568", "180499a0-dc28-4f94-9ed8-c9583aa1723a", "Shipper", "SHIPPER" }
                });
        }
    }
}
