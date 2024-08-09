using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResidentWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPasswordColumnLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EvacuationCenter",
                schema: "dbo",
                columns: table => new
                {
                    CenterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvacuationCenter", x => x.CenterID);
                });

            migrationBuilder.CreateTable(
                name: "Resident",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurokNumber = table.Column<int>(type: "int", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resident", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resident_Username",
                table: "Resident",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvacuationCenter",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Resident");
        }
    }
}
