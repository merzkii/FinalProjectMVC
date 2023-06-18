using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Data.Migrations
{
    public partial class CreateTransactionHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "TransactionHistory",
            columns: table => new
            {
                TransactionId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(nullable: false, maxLength: 450),
                Amount = table.Column<decimal>(nullable: false),
                TransactionType = table.Column<string>(nullable: false),
                TransactionDate = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionHistory", x => x.TransactionId);
                table.ForeignKey(
                    name: "FK_TransactionHistory_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_UserId",
                table: "TransactionHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "TransactionHistory");
        }
    }
}
