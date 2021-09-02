using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDataInContacsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Contacts values('Annem','+905052532569')");
            migrationBuilder.Sql("insert into Contacts values('Babam','+905380158608')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
