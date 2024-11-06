using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.data.Migrations
{
    /// <inheritdoc />
    public partial class addNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToLocalIdName",
                table: "modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fromPrimaryKeyName",
                table: "modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToLocalIdName",
                table: "modules");

            migrationBuilder.DropColumn(
                name: "fromPrimaryKeyName",
                table: "modules");
        }
    }
}
