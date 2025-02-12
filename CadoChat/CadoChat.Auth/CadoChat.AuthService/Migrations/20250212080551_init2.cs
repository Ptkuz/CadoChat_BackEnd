using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CadoChat.AuthService.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "265b5ed7-321d-4228-a1f2-540c1686a910");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39a5971e-f6cd-4dd7-b9c8-3bfc50717f17");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "945e5c4f-9d07-4594-abe6-a7529057e3f0", null, "User", "USER" },
                    { "e7b8bc1c-9474-4202-b565-f75f1d734d01", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "945e5c4f-9d07-4594-abe6-a7529057e3f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7b8bc1c-9474-4202-b565-f75f1d734d01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "265b5ed7-321d-4228-a1f2-540c1686a910", null, "Admin", "ADMIN" },
                    { "39a5971e-f6cd-4dd7-b9c8-3bfc50717f17", null, "User", "USER" }
                });
        }
    }
}
