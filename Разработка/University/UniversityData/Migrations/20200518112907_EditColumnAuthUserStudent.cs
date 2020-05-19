using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityData.Migrations
{
    public partial class EditColumnAuthUserStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "auth_user_student_pkey",
                table: "auth_user_student");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "auth_user_student",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "auth_user_student_pkey",
                table: "auth_user_student",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "auth_user_student_pkey",
                table: "auth_user_student");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "auth_user_student",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "auth_user_student_pkey",
                table: "auth_user_student",
                column: "Id");
        }
    }
}
