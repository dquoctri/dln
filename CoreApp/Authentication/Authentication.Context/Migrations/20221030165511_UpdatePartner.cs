using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Context.Migrations
{
    public partial class UpdatePartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizers_Organizers_OrganizerId",
                schema: "dln_auth",
                table: "Organizers");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                schema: "dln_auth",
                table: "Organizers",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizers_OrganizerId",
                schema: "dln_auth",
                table: "Organizers",
                newName: "IX_Organizers_ProfileId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dln_auth",
                table: "Partners",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dln_auth",
                table: "Partners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizers_Profiles_ProfileId",
                schema: "dln_auth",
                table: "Organizers",
                column: "ProfileId",
                principalSchema: "dln_auth",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizers_Profiles_ProfileId",
                schema: "dln_auth",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dln_auth",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "dln_auth",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                schema: "dln_auth",
                table: "Organizers",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizers_ProfileId",
                schema: "dln_auth",
                table: "Organizers",
                newName: "IX_Organizers_OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizers_Organizers_OrganizerId",
                schema: "dln_auth",
                table: "Organizers",
                column: "OrganizerId",
                principalSchema: "dln_auth",
                principalTable: "Organizers",
                principalColumn: "Id");
        }
    }
}
