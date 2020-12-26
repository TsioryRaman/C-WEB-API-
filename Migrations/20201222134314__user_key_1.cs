using Microsoft.EntityFrameworkCore.Migrations;

namespace Tache.Migrations
{
    public partial class _user_key_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Users",
                newName: "Id");
        }
    }
}
