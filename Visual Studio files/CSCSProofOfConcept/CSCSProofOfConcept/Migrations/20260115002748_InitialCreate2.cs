using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSCSProofOfConcept.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_DistributionCenter_DistributionCenterId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "DistributionCenterId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_DistributionCenter_DistributionCenterId",
                table: "Item",
                column: "DistributionCenterId",
                principalTable: "DistributionCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_DistributionCenter_DistributionCenterId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "DistributionCenterId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_DistributionCenter_DistributionCenterId",
                table: "Item",
                column: "DistributionCenterId",
                principalTable: "DistributionCenter",
                principalColumn: "Id");
        }
    }
}
