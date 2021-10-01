using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingTrails.Api.Persistence.Data.Migrations
{
    public partial class AddOwnerToTrail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Trails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Trails");
        }
    }
}
