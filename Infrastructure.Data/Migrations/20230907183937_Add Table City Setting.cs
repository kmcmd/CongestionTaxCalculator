using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddTableCitySetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitySetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    MaximunFeePerDay = table.Column<int>(nullable: false),
                    MinimumTimeCalcuteFee = table.Column<int>(nullable: false),
                    FreeMonthList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitySetting", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CitySetting",
                columns: new[] { "Id", "FreeMonthList", "MaximunFeePerDay", "MinimumTimeCalcuteFee", "Name" },
                values: new object[] { 1, "[7]", 60, 60, "Gothenburg" });

            migrationBuilder.InsertData(
                table: "CitySetting",
                columns: new[] { "Id", "FreeMonthList", "MaximunFeePerDay", "MinimumTimeCalcuteFee", "Name" },
                values: new object[] { 2, null, 80, 30, "London" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitySetting");
        }
    }
}
