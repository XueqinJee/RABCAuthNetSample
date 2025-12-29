using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class 用户新增昵称栏位 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nick_name",
                table: "user",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "",
                comment: "昵称");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nick_name",
                table: "user");
        }
    }
}
