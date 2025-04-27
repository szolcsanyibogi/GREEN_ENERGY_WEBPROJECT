using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GREEN_ENERGY_WEBPROJECT.Endpoint.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstDiagramContentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FifthDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FifthDiagramContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirstDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstDiagramContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FourthDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FourthDiagramContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecondDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondDiagramContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SixthDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SixthDiagramContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThirdDiagramContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdDiagramContents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FifthDiagramContents");

            migrationBuilder.DropTable(
                name: "FirstDiagramContents");

            migrationBuilder.DropTable(
                name: "FourthDiagramContents");

            migrationBuilder.DropTable(
                name: "SecondDiagramContents");

            migrationBuilder.DropTable(
                name: "SixthDiagramContents");

            migrationBuilder.DropTable(
                name: "ThirdDiagramContents");
        }
    }
}
