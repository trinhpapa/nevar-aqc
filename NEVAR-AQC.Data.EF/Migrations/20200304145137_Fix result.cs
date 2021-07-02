using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NEVAR_AQC.Data.EF.Migrations
{
    public partial class Fixresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceResultDate",
                table: "IDTestRequirement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceResultDate",
                table: "IDTestRequirement");
        }
    }
}
