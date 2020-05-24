using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class PopulateBaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Customers (Name, CompanyName, Address) VALUES ('Frants','ECIT Services','2730 Herlev, Copenhagen')");
            migrationBuilder.Sql("INSERT INTO Customers (Name, CompanyName, Address) VALUES ('Bence','Reviso ApS','Ewaldsgade 3, 2200 København')");
            migrationBuilder.Sql("INSERT INTO Customers (Name, CompanyName, Address) VALUES ('Willy Wonka','The Chocolate Factory','Birmingham, Warwickshire, England')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Customers WHERE Name IN ('Frants', 'Bence', 'Willy Wonka')");
        }
    }
}
