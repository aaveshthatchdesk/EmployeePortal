using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_designations_departments_DepartmentId",
                table: "designations");

            migrationBuilder.DropIndex(
                name: "IX_designations_DepartmentId",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "designations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "designations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_designations_DepartmentId",
                table: "designations",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_designations_departments_DepartmentId",
                table: "designations",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");
        }
    }
}
