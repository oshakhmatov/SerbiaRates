using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SerbiaRates.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRatesCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "ExchangeRates",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ExchangeRates");
        }
    }
}
