using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcAuthNetSample.Core.Migrations
{
    /// <inheritdoc />
    public partial class 新增菜单角色表 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    parent_id = table.Column<int>(type: "int", nullable: false, comment: "父菜单ID（顶级菜单填0）"),
                    menu_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "菜单名称（显示用）"),
                    menu_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, comment: "菜单类型（1=目录，2=菜单，3=按钮）"),
                    path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "前端路由路径（菜单类型为2时必填）"),
                    component = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "前端组件路径（Vue/React组件地址，菜单类型为2时必填）"),
                    permission = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "权限标识（按钮类型必填，格式：模块:功能:操作）"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序号（数字越小越靠前）"),
                    is_enabled = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用（true=启用，false=禁用）"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单图标（如el-icon-user）"),
                    is_external = table.Column<bool>(type: "bit", nullable: false, comment: "是否外链（true=是，false=否）"),
                    is_hide = table.Column<bool>(type: "bit", nullable: false, comment: "是否隐藏菜单（true=隐藏，false=显示）"),
                    redirect = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "路由重定向地址（目录类型常用）"),
                    remark = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "菜单备注/说明"),
                    id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    update_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "修改时间"),
                    created_by_id = table.Column<int>(type: "int", nullable: false, comment: "建立人"),
                    updated_by_id = table.Column<int>(type: "int", nullable: false, comment: "修改人")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                },
                comment: "系统菜单表（含目录、菜单、按钮权限）");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色名称（如超级管理员、普通用户）"),
                    role_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色编码（全局唯一，用于权限校验）"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "角色描述/备注（说明角色用途）"),
                    is_system = table.Column<bool>(type: "bit", nullable: false, comment: "是否系统内置角色（内置角色不可删除/修改编码）"),
                    is_enabled = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用（禁用后角色关联的权限失效）"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序号（数字越小越靠前）"),
                    id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    update_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "修改时间"),
                    created_by_id = table.Column<int>(type: "int", nullable: false, comment: "建立人"),
                    updated_by_id = table.Column<int>(type: "int", nullable: false, comment: "修改人")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                },
                comment: "系统角色表（用于权限分配）");

            migrationBuilder.CreateTable(
                name: "role_menu",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false, comment: "角色ID"),
                    menu_id = table.Column<int>(type: "int", nullable: false, comment: "菜单ID"),
                    id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    update_on = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "修改时间"),
                    created_by_id = table.Column<int>(type: "int", nullable: false, comment: "建立人"),
                    updated_by_id = table.Column<int>(type: "int", nullable: false, comment: "修改人")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_menu", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_menu_menu_menu_id",
                        column: x => x.menu_id,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_menu_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "角色菜单关联表（多对多）");

            migrationBuilder.CreateIndex(
                name: "IX_role_menu_menu_id",
                table: "role_menu",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_menu_role_id",
                table: "role_menu",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_menu");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
