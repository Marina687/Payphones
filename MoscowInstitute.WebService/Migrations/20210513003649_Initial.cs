using Microsoft.EntityFrameworkCore.Migrations;

namespace MoscowPayphones.WebService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payphones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescriptionLocation = table.Column<string>(nullable: true),
                    PayWay = table.Column<string>(nullable: true),
                    IntercityConnectionPayment = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ValidUniversalServicesCard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payphones", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Payphones",
                columns: new[] { "Id", "DescriptionLocation", "IntercityConnectionPayment", "Name", "PayWay", "ValidUniversalServicesCard" },
                values: new object[] { 1L, "Вавилова улица, дом 5А", "бесплатно", "Таксофон № 1449", "карта", "не действует" });

            migrationBuilder.InsertData(
                table: "Payphones",
                columns: new[] { "Id", "DescriptionLocation", "IntercityConnectionPayment", "Name", "PayWay", "ValidUniversalServicesCard" },
                values: new object[] { 2L, "Вавилова улица, дом 51, Школа №199", "бесплатно", "Таксофон № 1499", "карта", "не действует" });

            migrationBuilder.InsertData(
                table: "Payphones",
                columns: new[] { "Id", "DescriptionLocation", "IntercityConnectionPayment", "Name", "PayWay", "ValidUniversalServicesCard" },
                values: new object[] { 3L, "Вавилова улица, дом 6", "бесплатно", "Таксофон № 76", "карта", "не действует" });

            migrationBuilder.InsertData(
                table: "Payphones",
                columns: new[] { "Id", "DescriptionLocation", "IntercityConnectionPayment", "Name", "PayWay", "ValidUniversalServicesCard" },
                values: new object[] { 4L, "Валдайский проезд, дом 14, Школа №158", "бесплатно", "Таксофон № 1857", "карта", "не действует" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payphones");
        }
    }
}
