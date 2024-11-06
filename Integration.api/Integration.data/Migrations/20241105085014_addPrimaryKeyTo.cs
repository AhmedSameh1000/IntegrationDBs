using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.data.Migrations
{
    /// <inheritdoc />
    public partial class addPrimaryKeyTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToPrimaryKeyName",
                table: "modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToPrimaryKeyName",
                table: "modules");
        }
    }
}
