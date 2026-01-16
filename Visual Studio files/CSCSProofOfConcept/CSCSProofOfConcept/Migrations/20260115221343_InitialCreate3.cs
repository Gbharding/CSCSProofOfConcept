using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSCSProofOfConcept.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Item_ItemId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Item_ItemId",
                table: "Project",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Item_ItemId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Project",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Item_ItemId",
                table: "Project",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }
    }
}
