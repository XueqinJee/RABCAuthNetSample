using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class 密码字段长度调整 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "salt",
                table: "user",
                type: "varchar(64)",
                nullable: true,
                comment: "密钥的盐",
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true,
                oldComment: "密钥的盐");

            migrationBuilder.AlterColumn<string>(
                name: "avator",
                table: "user",
                type: "varchar(256)",
                nullable: true,
                comment: "头像",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true,
                oldComment: "头像");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "salt",
                table: "user",
                type: "varchar(32)",
                nullable: true,
                comment: "密钥的盐",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true,
                oldComment: "密钥的盐");

            migrationBuilder.AlterColumn<string>(
                name: "avator",
                table: "user",
                type: "varchar(64)",
                nullable: true,
                comment: "头像",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldNullable: true,
                oldComment: "头像");
        }
    }
}
