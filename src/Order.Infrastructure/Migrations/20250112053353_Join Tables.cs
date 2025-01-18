using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductSize",
                table: "ProductProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductSize_ProductsId",
                table: "ProductProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductColor",
                table: "ProductProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductColor_ProductsId",
                table: "ProductProductColor");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSizes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductColors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductColors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductSize",
                table: "ProductProductSize",
                columns: new[] { "ProductsId", "ProductSizesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductColor",
                table: "ProductProductColor",
                columns: new[] { "ProductsId", "ProductColorsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductSize_ProductSizesId",
                table: "ProductProductSize",
                column: "ProductSizesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductColor_ProductColorsId",
                table: "ProductProductColor",
                column: "ProductColorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductSize",
                table: "ProductProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductSize_ProductSizesId",
                table: "ProductProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductColor",
                table: "ProductProductColor");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductColor_ProductColorsId",
                table: "ProductProductColor");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSizes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductColors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductColors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductSize",
                table: "ProductProductSize",
                columns: new[] { "ProductSizesId", "ProductsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductColor",
                table: "ProductProductColor",
                columns: new[] { "ProductColorsId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductSize_ProductsId",
                table: "ProductProductSize",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductColor_ProductsId",
                table: "ProductProductColor",
                column: "ProductsId");
        }
    }
}
