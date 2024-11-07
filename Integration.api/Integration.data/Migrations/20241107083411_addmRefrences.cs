using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.data.Migrations
{
    /// <inheritdoc />
    public partial class addmRefrences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "References",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_References_ModuleId",
                table: "References",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_References_modules_ModuleId",
                table: "References",
                column: "ModuleId",
                principalTable: "modules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_References_modules_ModuleId",
                table: "References");

            migrationBuilder.DropIndex(
                name: "IX_References_ModuleId",
                table: "References");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "References");
        }
    }
}
