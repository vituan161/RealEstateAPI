using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAPI.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_AgencyCompany_AgencyCompanyId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoB",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCompanyId",
                table: "Seller",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DoB",
                table: "Profile",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_AgencyCompany_AgencyCompanyId",
                table: "Seller",
                column: "AgencyCompanyId",
                principalTable: "AgencyCompany",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_AgencyCompany_AgencyCompanyId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "DoB",
                table: "Profile");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyCompanyId",
                table: "Seller",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DoB",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_AgencyCompany_AgencyCompanyId",
                table: "Seller",
                column: "AgencyCompanyId",
                principalTable: "AgencyCompany",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
