using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourFee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<long>(nullable: false),
                    End = table.Column<long>(nullable: false),
                    FeeAmount = table.Column<int>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehicleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 1, 233400000000L, 8, 216000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 10, 215400000000L, 0, 666000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 8, 647400000000L, 13, 612000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 7, 611400000000L, 18, 558000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 6, 557400000000L, 13, 540000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 9, 665400000000L, 8, 648000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 4, 305400000000L, 13, 288000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 3, 287400000000L, 18, 252000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 2, 251400000000L, 13, 234000000000L });

            migrationBuilder.InsertData(
                table: "HourFee",
                columns: new[] { "Id", "End", "FeeAmount", "Start" },
                values: new object[] { 5, 539400000000L, 8, 306000000000L });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 7, 7 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 4, 6 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 5, 5 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 6, 4 });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "VehicleType" },
                values: new object[] { 8, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourFee");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
