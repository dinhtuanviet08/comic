using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "RentalDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rentals",
                newName: "RentalId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RentalDetails",
                newName: "RentalDetailId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Rentals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "PricePerDay",
                table: "RentalDetails",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "RentalId",
                table: "Rentals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RentalDetailId",
                table: "RentalDetails",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerDay",
                table: "RentalDetails",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "RentalDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
