using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dbModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lat = table.Column<double>(type: "float", nullable: false),
                    lng = table.Column<double>(type: "float", nullable: false),
                    iso2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    iso3 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    admin_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    city_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    city_id = table.Column<int>(type: "int", nullable: false),
                    restaurant_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    restaurant_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    review_score = table.Column<double>(type: "float", nullable: false),
                    review_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
