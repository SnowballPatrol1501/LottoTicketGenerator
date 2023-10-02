using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class initiallottoticketdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LottoTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SuperNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    ShowSuperNumber = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LottoTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LottoTicketBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numbers_Csv = table.Column<string>(type: "TEXT", nullable: false),
                    LottoTicketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LottoTicketBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LottoTicketBoxes_LottoTickets_LottoTicketId",
                        column: x => x.LottoTicketId,
                        principalTable: "LottoTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LottoTicketBoxes_LottoTicketId",
                table: "LottoTicketBoxes",
                column: "LottoTicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LottoTicketBoxes");

            migrationBuilder.DropTable(
                name: "LottoTickets");
        }
    }
}
