using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopCenter.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Storage",
                table: "Products",
                newName: "GraphicsCard");

            migrationBuilder.RenameColumn(
                name: "Processor",
                table: "Products",
                newName: "CPU");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "GraphicsCard",
                table: "Products",
                newName: "Storage");

            migrationBuilder.RenameColumn(
                name: "CPU",
                table: "Products",
                newName: "Processor");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
