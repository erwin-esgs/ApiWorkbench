using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWorkbench.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodPressureRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuccessEvent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressureRules", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BloodPressureRules",
                columns: new[] { "Id", "Expression", "RuleName", "SuccessEvent" },
                values: new object[,]
                {
                    { 1, "( Sistolik <= 120 && Sistolik >= 90 ) && ( Diastolik <= 80 && Diastolik >= 60 )", "BP.Normal", "Normal" },
                    { 2, "Sistolik < 90 && Diastolik < 60", "BP.HypoTension", "Hypotension" },
                    { 3, "Sistolik > 120 && Diastolik > 80", "BP.HyperTension", "Hypertension" },
                    { 4, "Sistolik > 120 || Diastolik > 80", "BP.PreHyperTension", "Prehypertension" },
                    { 5, "Sistolik < 90 || Diastolik < 60", "BP.PreHypoTension", "Prehypotension" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressureRules");
        }
    }
}
