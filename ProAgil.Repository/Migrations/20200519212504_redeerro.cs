using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class redeerro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociais_PalestranteId",
                table: "RedesSociais");

            migrationBuilder.DropColumn(
                name: "PalestranteId",
                table: "RedesSociais");

            migrationBuilder.AddColumn<int>(
                name: "PalestranteId1",
                table: "RedesSociais",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_PalestranteId1",
                table: "RedesSociais",
                column: "PalestranteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId1",
                table: "RedesSociais",
                column: "PalestranteId1",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId1",
                table: "RedesSociais");

            migrationBuilder.DropIndex(
                name: "IX_RedesSociais_PalestranteId1",
                table: "RedesSociais");

            migrationBuilder.DropColumn(
                name: "PalestranteId1",
                table: "RedesSociais");

            migrationBuilder.AddColumn<int>(
                name: "PalestranteId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_PalestranteId",
                table: "RedesSociais",
                column: "PalestranteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
