using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegfyInsurance.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PolicyNumber = table.Column<string>(type: "TEXT", nullable: false),
                    InsuredDocument = table.Column<string>(type: "TEXT", nullable: false),
                    VehiclePlate = table.Column<string>(type: "TEXT", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "REAL", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_PolicyNumber",
                table: "InsurancePolicies",
                column: "PolicyNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicies");
        }
    }
}
