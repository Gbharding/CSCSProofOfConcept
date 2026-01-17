using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSCSProofOfConcept.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldSpec",
                table: "Project",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "DistributionCenterId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_DistributionCenterId",
                table: "Project",
                column: "DistributionCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_DistributionCenter_DistributionCenterId",
                table: "Project",
                column: "DistributionCenterId",
                principalTable: "DistributionCenter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_DistributionCenter_DistributionCenterId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_DistributionCenterId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DistributionCenterId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Project",
                newName: "OldSpec");
        }
    }
}
