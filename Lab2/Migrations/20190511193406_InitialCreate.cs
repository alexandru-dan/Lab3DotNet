using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Expensess",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Description = table.Column<string>(nullable: true),
            //        Sum = table.Column<double>(nullable: false),
            //        Location = table.Column<string>(nullable: true),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Currency = table.Column<string>(nullable: true),
            //        Type = table.Column<string>(nullable: true)
            //    },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Expensess", x => x.Id);
            //        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expensess");
        }
    }
}
