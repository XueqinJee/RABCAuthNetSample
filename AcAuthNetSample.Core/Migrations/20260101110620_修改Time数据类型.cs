using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class 修改Time数据类型 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_on",
                table: "user",
                type: "datetimeoffset",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_on",
                table: "user",
                type: "datetimeoffset",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_on",
                table: "role_menu",
                type: "datetimeoffset",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_on",
                table: "role_menu",
                type: "datetimeoffset",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_on",
                table: "role",
                type: "datetimeoffset",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_on",
                table: "role",
                type: "datetimeoffset",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_on",
                table: "menu",
                type: "datetimeoffset",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_on",
                table: "menu",
                type: "datetimeoffset",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_on",
                table: "access_token",
                type: "datetimeoffset",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "login_on",
                table: "access_token",
                type: "datetimeoffset",
                nullable: false,
                comment: "登录时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "登录时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "expire_time",
                table: "access_token",
                type: "datetimeoffset",
                nullable: false,
                comment: "登录过期时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "登录过期时间");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_on",
                table: "access_token",
                type: "datetimeoffset",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "创建时间");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_on",
                table: "user",
                type: "datetime2",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "user",
                type: "datetime2",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_on",
                table: "role_menu",
                type: "datetime2",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "role_menu",
                type: "datetime2",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_on",
                table: "role",
                type: "datetime2",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "role",
                type: "datetime2",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_on",
                table: "menu",
                type: "datetime2",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "menu",
                type: "datetime2",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_on",
                table: "access_token",
                type: "datetime2",
                nullable: false,
                comment: "修改时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "login_on",
                table: "access_token",
                type: "datetime2",
                nullable: false,
                comment: "登录时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "登录时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expire_time",
                table: "access_token",
                type: "datetime2",
                nullable: false,
                comment: "登录过期时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "登录过期时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "access_token",
                type: "datetime2",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComment: "创建时间");
        }
    }
}
