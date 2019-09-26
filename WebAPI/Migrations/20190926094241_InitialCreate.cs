using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PcData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PcName = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true),
                    CpuLoad = table.Column<string>(nullable: true),
                    RamLoad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoggedUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LogginDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    DataModelPKey = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggedUser_PcData_DataModelPKey",
                        column: x => x.DataModelPKey,
                        principalTable: "PcData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CPU = table.Column<string>(nullable: true),
                    MotherBoard = table.Column<string>(nullable: true),
                    GPU = table.Column<string>(nullable: true),
                    DataModelPKey = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturer_PcData_DataModelPKey",
                        column: x => x.DataModelPKey,
                        principalTable: "PcData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggedUser_DataModelPKey",
                table: "LoggedUser",
                column: "DataModelPKey");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_DataModelPKey",
                table: "Manufacturer",
                column: "DataModelPKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggedUser");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "PcData");
        }
    }
}
