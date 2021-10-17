using Microsoft.EntityFrameworkCore.Migrations;

namespace Lee_Stephens_Assignment1_COMP2084.Data.Migrations
{
    public partial class UpdatingItemAndSectionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemSectionId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionsSectionId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "InventoryItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemSection = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SectionsSectionId",
                table: "Items",
                column: "SectionsSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Section_SectionsSectionId",
                table: "Items",
                column: "SectionsSectionId",
                principalTable: "Section",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Section_SectionsSectionId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Items_SectionsSectionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemSectionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SectionsSectionId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
