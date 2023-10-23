using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewSupplierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderPurchase_SupplierId",
                table: "OrderPurchase");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPurchase_SupplierId",
                table: "OrderPurchase",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderPurchase_SupplierId",
                table: "OrderPurchase");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPurchase_SupplierId",
                table: "OrderPurchase",
                column: "SupplierId",
                unique: true);
        }
    }
}
