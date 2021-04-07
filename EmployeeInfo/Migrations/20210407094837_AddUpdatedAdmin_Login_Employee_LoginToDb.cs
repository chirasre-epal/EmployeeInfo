using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeInfo.Migrations
{
    public partial class AddUpdatedAdmin_Login_Employee_LoginToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Employee_pass",
                table: "Employees_Login",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Employees_Login",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Admin_pass",
                table: "Admins_Login",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Admin_id",
                table: "Admins_Login",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employees_Login",
                newName: "Employee_pass");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees_Login",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Admins_Login",
                newName: "Admin_pass");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Admins_Login",
                newName: "Admin_id");
        }
    }
}
