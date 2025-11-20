using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipNet.Migrations
{
    /// <inheritdoc />
    public partial class AddRemoteField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemote",
                table: "Internships",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Internships",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsRemote",
                value: false);

            migrationBuilder.UpdateData(
                table: "Internships",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsRemote",
                value: false);

            migrationBuilder.UpdateData(
                table: "StudentApplications",
                keyColumns: new[] { "InternshipId", "StudentId" },
                keyValues: new object[] { 1, 1 },
                column: "AppliedDate",
                value: new DateTime(2025, 11, 20, 18, 54, 55, 569, DateTimeKind.Utc).AddTicks(3435));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemote",
                table: "Internships");

            migrationBuilder.UpdateData(
                table: "StudentApplications",
                keyColumns: new[] { "InternshipId", "StudentId" },
                keyValues: new object[] { 1, 1 },
                column: "AppliedDate",
                value: new DateTime(2025, 11, 20, 18, 30, 1, 615, DateTimeKind.Utc).AddTicks(5218));
        }
    }
}
