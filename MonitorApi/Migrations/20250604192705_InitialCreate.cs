using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitorApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitorTabela",
                columns: table => new
                {
                    IdMonitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorTabela", x => x.IdMonitor);
                });

            migrationBuilder.CreateTable(
                name: "HorarioTabela",
                columns: table => new
                {
                    IdHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HorarioAtendimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMonitor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioTabela", x => x.IdHorario);
                    table.ForeignKey(
                        name: "FK_HorarioTabela_MonitorTabela_IdMonitor",
                        column: x => x.IdMonitor,
                        principalTable: "MonitorTabela",
                        principalColumn: "IdMonitor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioTabela_IdMonitor",
                table: "HorarioTabela",
                column: "IdMonitor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioTabela");

            migrationBuilder.DropTable(
                name: "MonitorTabela");
        }
    }
}
