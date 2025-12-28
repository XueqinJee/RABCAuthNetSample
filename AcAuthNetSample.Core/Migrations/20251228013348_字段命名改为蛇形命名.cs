using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class 字段命名改为蛇形命名 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_token_user_UserId",
                table: "access_token");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "user",
                newName: "salt");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "user",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "user",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Avator",
                table: "user",
                newName: "avator");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "user",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "user",
                newName: "updated_by_id");

            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "user",
                newName: "update_on");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "user",
                newName: "created_by_id");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "user",
                newName: "create_on");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "access_token",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "access_token",
                newName: "ip");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "access_token",
                newName: "client");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "access_token",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "access_token",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UserAgent",
                table: "access_token",
                newName: "user_agent");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "access_token",
                newName: "updated_by_id");

            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "access_token",
                newName: "update_on");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "access_token",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "LoginOn",
                table: "access_token",
                newName: "login_on");

            migrationBuilder.RenameColumn(
                name: "FailCount",
                table: "access_token",
                newName: "fail_count");

            migrationBuilder.RenameColumn(
                name: "ExpireTime",
                table: "access_token",
                newName: "expire_time");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "access_token",
                newName: "created_by_id");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "access_token",
                newName: "create_on");

            migrationBuilder.RenameIndex(
                name: "IX_access_token_UserId",
                table: "access_token",
                newName: "IX_access_token_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_access_token_user_user_id",
                table: "access_token",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_token_user_user_id",
                table: "access_token");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "user",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "user",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "user",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "avator",
                table: "user",
                newName: "Avator");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "user",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "updated_by_id",
                table: "user",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "update_on",
                table: "user",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "created_by_id",
                table: "user",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "user",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "access_token",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "ip",
                table: "access_token",
                newName: "Ip");

            migrationBuilder.RenameColumn(
                name: "client",
                table: "access_token",
                newName: "Client");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "access_token",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "access_token",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_agent",
                table: "access_token",
                newName: "UserAgent");

            migrationBuilder.RenameColumn(
                name: "updated_by_id",
                table: "access_token",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "update_on",
                table: "access_token",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "access_token",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "login_on",
                table: "access_token",
                newName: "LoginOn");

            migrationBuilder.RenameColumn(
                name: "fail_count",
                table: "access_token",
                newName: "FailCount");

            migrationBuilder.RenameColumn(
                name: "expire_time",
                table: "access_token",
                newName: "ExpireTime");

            migrationBuilder.RenameColumn(
                name: "created_by_id",
                table: "access_token",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "access_token",
                newName: "CreateOn");

            migrationBuilder.RenameIndex(
                name: "IX_access_token_user_id",
                table: "access_token",
                newName: "IX_access_token_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_access_token_user_UserId",
                table: "access_token",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
