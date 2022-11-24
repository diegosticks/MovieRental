using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental.Migrations
{
    public partial class SetNameToMembershipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Pay as You Go' WHERE Id = 1");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE Id = 2");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE Id = 3");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Annual' WHERE Id = 4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
