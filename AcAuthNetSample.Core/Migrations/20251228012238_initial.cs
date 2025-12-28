using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(32)", nullable: false, comment: "用户名"),
                    Password = table.Column<string>(type: "varchar(128)", nullable: false),
                    Salt = table.Column<string>(type: "varchar(32)", nullable: true, comment: "密钥的盐"),
                    Avator = table.Column<string>(type: "varchar(64)", nullable: true, comment: "头像"),
                    Email = table.Column<string>(type: "varchar(32)", nullable: true, comment: "邮箱"),
                    Phone = table.Column<string>(type: "varchar(15)", nullable: true, comment: "手机号"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true, comment: "描述"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "修改时间"),
                    CreatedById = table.Column<int>(type: "int", nullable: false, comment: "建立人"),
                    UpdatedById = table.Column<int>(type: "int", nullable: false, comment: "修改人")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "access_token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserAgent = table.Column<string>(type: "varchar(128)", nullable: false, comment: "登录设备"),
                    Client = table.Column<string>(type: "varchar(32)", nullable: false, comment: "登录平台，Android、IOS、Windows等"),
                    Ip = table.Column<string>(type: "varchar(128)", nullable: false, comment: "登录IP"),
                    Token = table.Column<string>(type: "varchar(max)", nullable: true, comment: "Token"),
                    RefreshToken = table.Column<string>(type: "varchar(max)", nullable: true, comment: "RefreshToken"),
                    FailCount = table.Column<int>(type: "int", nullable: false, comment: "失败次数"),
                    LoginOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "登录时间"),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "登录过期时间"),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "修改时间"),
                    CreatedById = table.Column<int>(type: "int", nullable: false, comment: "建立人"),
                    UpdatedById = table.Column<int>(type: "int", nullable: false, comment: "修改人")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_access_token_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_token_UserId",
                table: "access_token",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_token");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
