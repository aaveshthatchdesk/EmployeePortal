using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_employees_EmployeeId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Designation_Department_DepartmentId",
                table: "Designation");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_Department_DepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_Designation_DesignationId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Designation",
                table: "Designation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "Designation",
                newName: "designations");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "departments");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "attendances");

            migrationBuilder.RenameIndex(
                name: "IX_Designation_DepartmentId",
                table: "designations",
                newName: "IX_designations_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_EmployeeId",
                table: "attendances",
                newName: "IX_attendances_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_designations",
                table: "designations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attendances",
                table: "attendances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_employees_EmployeeId",
                table: "attendances",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_designations_departments_DepartmentId",
                table: "designations",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_designations_DesignationId",
                table: "employees",
                column: "DesignationId",
                principalTable: "designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_employees_EmployeeId",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_designations_departments_DepartmentId",
                table: "designations");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_designations_DesignationId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_designations",
                table: "designations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_attendances",
                table: "attendances");

            migrationBuilder.RenameTable(
                name: "designations",
                newName: "Designation");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "attendances",
                newName: "Attendance");

            migrationBuilder.RenameIndex(
                name: "IX_designations_DepartmentId",
                table: "Designation",
                newName: "IX_Designation_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_EmployeeId",
                table: "Attendance",
                newName: "IX_Attendance_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Designation",
                table: "Designation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_employees_EmployeeId",
                table: "Attendance",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designation_Department_DepartmentId",
                table: "Designation",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Department_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Designation_DesignationId",
                table: "employees",
                column: "DesignationId",
                principalTable: "Designation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
