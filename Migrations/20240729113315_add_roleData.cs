using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthSample.Migrations
{
    /// <inheritdoc />
    public partial class add_roleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a9e4508-0078-44f7-ae1e-ce07d65a84be"), null, "Admin", "Admin" },
                    { new Guid("93779de5-302c-4dfc-ac5f-b3dccc155f3d"), null, "Hmpy", "基本權限" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a9e4508-0078-44f7-ae1e-ce07d65a84be"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93779de5-302c-4dfc-ac5f-b3dccc155f3d"));
        }
    }
}
