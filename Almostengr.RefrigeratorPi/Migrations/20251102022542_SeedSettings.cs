using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almostengr.RefrigeratorPi.Migrations
{
    /// <inheritdoc />
    public partial class SeedSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: ["Id", "Value", "ModifiedBy", "ModifiedDate"],
                values: new object[,]
                {
                    {1, "32", "SYSTEM", DateTime.Now},
                    {2, "36", "SYSTEM", DateTime.Now},
                    {3, "0", "SYSTEM", DateTime.Now},
                    {4, "90", "SYSTEM", DateTime.Now},
                    {8, "0", "SYSTEM", DateTime.Now},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "SystemSettings",
                "Id",
                keyValues: [1, 2, 3, 8]
            );
        }
    }
}
