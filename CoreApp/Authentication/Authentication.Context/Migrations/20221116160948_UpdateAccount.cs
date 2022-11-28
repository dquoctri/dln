using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganisationId",
                schema: "dln_auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dln_auth",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                schema: "dln_auth",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                schema: "dln_auth",
                table: "Users",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Profiles_ProfileId",
                schema: "dln_auth",
                table: "Users",
                column: "ProfileId",
                principalSchema: "dln_auth",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Profiles_ProfileId",
                schema: "dln_auth",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                schema: "dln_auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                schema: "dln_auth",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                schema: "dln_auth",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dln_auth",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
