using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssistantP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhysicianId",
                table: "Assistants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_PhysicianId",
                table: "Assistants",
                column: "PhysicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistants_AspNetUsers_PhysicianId",
                table: "Assistants",
                column: "PhysicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistants_AspNetUsers_PhysicianId",
                table: "Assistants");

            migrationBuilder.DropIndex(
                name: "IX_Assistants_PhysicianId",
                table: "Assistants");

            migrationBuilder.DropColumn(
                name: "PhysicianId",
                table: "Assistants");
        }
    }
}
