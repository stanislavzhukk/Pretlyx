using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContextMinorChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ownerProfiles_AspNetUsers_UserId",
                table: "ownerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ownerProfiles",
                table: "ownerProfiles");

            migrationBuilder.RenameTable(
                name: "ownerProfiles",
                newName: "OwnerProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_ownerProfiles_UserId",
                table: "OwnerProfiles",
                newName: "IX_OwnerProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerProfiles",
                table: "OwnerProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerProfiles_AspNetUsers_UserId",
                table: "OwnerProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerProfiles_AspNetUsers_UserId",
                table: "OwnerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerProfiles",
                table: "OwnerProfiles");

            migrationBuilder.RenameTable(
                name: "OwnerProfiles",
                newName: "ownerProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_OwnerProfiles_UserId",
                table: "ownerProfiles",
                newName: "IX_ownerProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ownerProfiles",
                table: "ownerProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ownerProfiles_AspNetUsers_UserId",
                table: "ownerProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
