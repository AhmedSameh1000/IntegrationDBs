using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integration.data.Migrations
{
    /// <inheritdoc />
    public partial class AddConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tableFroms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDbId = table.Column<int>(type: "int", nullable: false),
                    TableToId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tableFroms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tableFroms_fromDbs_FromDbId",
                        column: x => x.FromDbId,
                        principalTable: "fromDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "columnFroms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tableFromId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_columnFroms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_columnFroms_tableFroms_tableFromId",
                        column: x => x.tableFromId,
                        principalTable: "tableFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tableTos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDbId = table.Column<int>(type: "int", nullable: false),
                    TableFromId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tableTos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tableTos_tableFroms_TableFromId",
                        column: x => x.TableFromId,
                        principalTable: "tableFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tableTos_toDbs_ToDbId",
                        column: x => x.ToDbId,
                        principalTable: "toDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "columnTos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tableToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_columnTos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_columnTos_tableTos_tableToId",
                        column: x => x.tableToId,
                        principalTable: "tableTos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableFromId = table.Column<int>(type: "int", nullable: false),
                    TableToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modules_tableFroms_TableFromId",
                        column: x => x.TableFromId,
                        principalTable: "tableFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modules_tableTos_TableToId",
                        column: x => x.TableToId,
                        principalTable: "tableTos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conditions_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_columnFroms_tableFromId",
                table: "columnFroms",
                column: "tableFromId");

            migrationBuilder.CreateIndex(
                name: "IX_columnTos_tableToId",
                table: "columnTos",
                column: "tableToId");

            migrationBuilder.CreateIndex(
                name: "IX_conditions_ModuleId",
                table: "conditions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_modules_TableFromId",
                table: "modules",
                column: "TableFromId");

            migrationBuilder.CreateIndex(
                name: "IX_modules_TableToId",
                table: "modules",
                column: "TableToId");

            migrationBuilder.CreateIndex(
                name: "IX_tableFroms_FromDbId",
                table: "tableFroms",
                column: "FromDbId");

            migrationBuilder.CreateIndex(
                name: "IX_tableTos_TableFromId",
                table: "tableTos",
                column: "TableFromId",
                unique: true,
                filter: "[TableFromId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tableTos_ToDbId",
                table: "tableTos",
                column: "ToDbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "columnFroms");

            migrationBuilder.DropTable(
                name: "columnTos");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "tableTos");

            migrationBuilder.DropTable(
                name: "tableFroms");
        }
    }
}
