using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Migrations.ApplicationDbContext2Migrations
{
    /// <inheritdoc />
    public partial class removedphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "DetailsRestroo");

            migrationBuilder.AlterColumn<string>(
                name: "DetailedDescription",
                table: "DetailsRestroo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DetailedDescription",
                table: "DetailsRestroo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "DetailsRestroo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
