using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeInfo.Migrations
{
    public partial class AddAdmin_Login_Employee_LoginToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins_Login",
                columns: table => new
                {
                    Admin_id = table.Column<int>(type: "int", nullable: false),
                    Admin_pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins_Login", x => x.Admin_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees_Login",
                columns: table => new
                {
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Employee_pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees_Login", x => x.Employee_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins_Login");

            migrationBuilder.DropTable(
                name: "Employees_Login");
        }
    }
}
