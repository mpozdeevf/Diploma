using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniversityData.Migrations
{
    public partial class CreateTableAuthUserStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth_user_student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    username = table.Column<string>(maxLength: 20, nullable: true),
                    password = table.Column<string>(maxLength: 50, nullable: true),
                    salt = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_user_student_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "auth_user_student_id_fkey",
                        column: x => x.Id,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_user_student");
        }
    }
}
