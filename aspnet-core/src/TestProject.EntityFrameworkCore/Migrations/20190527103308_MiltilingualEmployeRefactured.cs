using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class MiltilingualEmployeRefactured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyLanguages_Employes_EmployeId",
                table: "MyLanguages");

            migrationBuilder.DropIndex(
                name: "IX_MyLanguages_EmployeId",
                table: "MyLanguages");

            migrationBuilder.DropColumn(
                name: "EmployeId",
                table: "MyLanguages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeId",
                table: "MyLanguages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyLanguages_EmployeId",
                table: "MyLanguages",
                column: "EmployeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyLanguages_Employes_EmployeId",
                table: "MyLanguages",
                column: "EmployeId",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
