using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Data.Migrations
{
    public partial class createtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Tokens",
                 columns: table => new
                 {
                     Id = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                     TokenValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     TokenPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Tokens", x => x.Id);
                     table.ForeignKey(
                         name: "FK_Tokens_AspNetUsers_UserId",
                         column: x => x.UserId,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                 });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Tokens");
        }
    }
}
