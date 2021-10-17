using Microsoft.EntityFrameworkCore.Migrations;

namespace Lee_Stephens_Assignment1_COMP2084.Data.Migrations
{
    public partial class AddLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "StoreLocation",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreLocation",
                table: "InventoryItems");

           
        }
    }
}
