using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TechInfoLookUp.Migrations
{
    public partial class ADD_TECH_TECHTAG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tech",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tech", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechTag",
                columns: table => new
                {
                    TechId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTag", x => new { x.TechId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TechTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechTag_Tech_TechId",
                        column: x => x.TechId,
                        principalTable: "Tech",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechTag_TagId",
                table: "TechTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechTag");

            migrationBuilder.DropTable(
                name: "Tech");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Tags");
        }
    }
}
