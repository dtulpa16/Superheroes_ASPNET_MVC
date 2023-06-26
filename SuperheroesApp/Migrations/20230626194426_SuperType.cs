using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperheroesApp.Migrations
{
    /// <inheritdoc />
    public partial class SuperType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuperTypeId",
                table: "Superheroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SuperType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Superheroes_SuperTypeId",
                table: "Superheroes",
                column: "SuperTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Superheroes_SuperType_SuperTypeId",
                table: "Superheroes",
                column: "SuperTypeId",
                principalTable: "SuperType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Superheroes_SuperType_SuperTypeId",
                table: "Superheroes");

            migrationBuilder.DropTable(
                name: "SuperType");

            migrationBuilder.DropIndex(
                name: "IX_Superheroes_SuperTypeId",
                table: "Superheroes");

            migrationBuilder.DropColumn(
                name: "SuperTypeId",
                table: "Superheroes");
        }
    }
}
