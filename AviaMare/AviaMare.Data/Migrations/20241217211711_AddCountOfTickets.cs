using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviaMare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCountOfTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Tickets");
        }
    }
}
