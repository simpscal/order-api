using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagescolumnintheProducttabletobeajson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.AddColumn<Dictionary<string, string[]>>(
                name: "ImagesByColor",
                table: "Products",
                type: "jsonb",
                nullable: false,
                defaultValue: new Dictionary<string, string[]>());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesByColor",
                table: "Products");

            migrationBuilder.AddColumn<string[]>(
                name: "Images",
                table: "Products",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
