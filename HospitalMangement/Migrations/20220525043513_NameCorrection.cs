using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalMangement.Migrations
{
    public partial class NameCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Hospitals",
                newName: "RegisterDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "Hospitals",
                newName: "CreatedDate");
        }
    }
}
