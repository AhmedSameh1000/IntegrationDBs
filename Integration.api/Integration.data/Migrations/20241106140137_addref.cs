using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.data.Migrations
{
    /// <inheritdoc />
    public partial class addref : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableFromId = table.Column<int>(type: "int", nullable: false),
                    TableToId = table.Column<int>(type: "int", nullable: false),
                    LocalPrimary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cloudLocalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloudPrimaryReferanceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_tableFroms_TableFromId",
                        column: x => x.TableFromId,
                        principalTable: "tableFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_References_tableTos_TableToId",
                        column: x => x.TableToId,
                        principalTable: "tableTos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_References_TableFromId",
                table: "References",
                column: "TableFromId");

            migrationBuilder.CreateIndex(
                name: "IX_References_TableToId",
                table: "References",
                column: "TableToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "References");
        }
    }
}
