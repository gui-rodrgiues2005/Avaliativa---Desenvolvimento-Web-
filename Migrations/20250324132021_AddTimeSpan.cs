using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avaliativa.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "Eventos",
                newName: "Data");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora",
                table: "Eventos",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Eventos",
                newName: "DataHora");
        }
    }
}
