using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRegistrationSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Brand = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    Type = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Year = table.Column<int>(type: "NUMBER(5)", nullable: false),
                    EngineCC = table.Column<int>(type: "NUMBER(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PlateNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IssueDate = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    RegistrationCity = table.Column<string>(type: "NVARCHAR2(35)", maxLength: 35, nullable: false),
                    OwnerName = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    OwnerCNIC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Registration_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
