using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Context.Migrations
{
    public partial class UpdateUniquePartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizers_PartnerId",
                schema: "dln_auth",
                table: "Organizers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dln_auth",
                table: "Partners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dln_auth",
                table: "Organizers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Name",
                schema: "dln_auth",
                table: "Partners",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_PartnerId_Name",
                schema: "dln_auth",
                table: "Organizers",
                columns: new[] { "PartnerId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Partners_Name",
                schema: "dln_auth",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Organizers_PartnerId_Name",
                schema: "dln_auth",
                table: "Organizers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dln_auth",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dln_auth",
                table: "Organizers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_PartnerId",
                schema: "dln_auth",
                table: "Organizers",
                column: "PartnerId");
        }
    }
}
