using Microsoft.EntityFrameworkCore.Migrations;

namespace Lesson6_HandsOn.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumArms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumHeads = table.Column<int>(type: "INTEGER", nullable: false),
                    NumLegs = table.Column<int>(type: "INTEGER", nullable: false),
                    BirthDate = table.Column<string>(type: "TEXT", nullable: true),
                    PlanetOfOrigin = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alien", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alien");
        }
    }
}
