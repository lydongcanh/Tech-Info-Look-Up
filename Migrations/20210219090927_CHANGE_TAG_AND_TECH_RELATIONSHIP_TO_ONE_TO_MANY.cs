using Microsoft.EntityFrameworkCore.Migrations;

namespace TechInfoLookUp.Migrations
{
    public partial class CHANGE_TAG_AND_TECH_RELATIONSHIP_TO_ONE_TO_MANY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tech",
                table: "Tech");

            migrationBuilder.RenameTable(
                name: "Tech",
                newName: "Techs");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Techs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Techs",
                table: "Techs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Techs_TagId",
                table: "Techs",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Techs_Tags_TagId",
                table: "Techs",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Techs_Tags_TagId",
                table: "Techs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Techs",
                table: "Techs");

            migrationBuilder.DropIndex(
                name: "IX_Techs_TagId",
                table: "Techs");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Techs");

            migrationBuilder.RenameTable(
                name: "Techs",
                newName: "Tech");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tech",
                table: "Tech",
                column: "Id");

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
    }
}
