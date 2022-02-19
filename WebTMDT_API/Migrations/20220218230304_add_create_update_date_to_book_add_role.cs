using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTMDT.Migrations
{
    public partial class add_create_update_date_to_book_add_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Books");
        }
    }
}
