using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class User表修改 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_disabled",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "user",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_role_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "is_disabled",
                table: "user");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "user");
        }
    }
}
