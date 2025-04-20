using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgsrTest.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_SearchPatientsByBirthDateFhir_Proc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText("Infrastructure/Migrations/Scripts/SearchPatientsByBirthDateFhir.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SearchPatientsByBirthDateFhir");
        }
    }
}
