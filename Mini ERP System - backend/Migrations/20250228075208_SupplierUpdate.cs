using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_ERP_System.Migrations
{
    /// <inheritdoc />
    public partial class SupplierUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuppplierName",
                table: "Suppliers",
                newName: "SupplierName");

            migrationBuilder.RenameColumn(
                name: "SuppliertId",
                table: "Suppliers",
                newName: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "Suppliers",
                newName: "SuppplierName");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Suppliers",
                newName: "SuppliertId");
        }
    }
}
