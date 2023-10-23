using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newSupplierTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPurchase_Supplier_SupplierId",
                table: "OrderPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPurchase_Supplier_SupplierId",
                table: "OrderPurchase",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPurchase_Supplier_SupplierId",
                table: "OrderPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPurchase_Supplier_SupplierId",
                table: "OrderPurchase",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
