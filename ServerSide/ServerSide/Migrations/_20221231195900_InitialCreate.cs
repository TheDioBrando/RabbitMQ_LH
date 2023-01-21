using ServerSide.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerSide.Migrations
{
    [DbContext(typeof(LibDbContext))]
    [Migration("20230101222800_InitialCreate")]
    public class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                },
                constraints: table => 
                {
                    table.PrimaryKey("PK_Library", l => l.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),        
                    Title = table.Column<string>(nullable:true),
                    LibraryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", b => b.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable:false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", u => u.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable:false),
                    BookId = table.Column<Guid>(nullable:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", o => o.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Libraries");
            migrationBuilder.DropTable("Books");
            migrationBuilder.DropTable("Users");
            migrationBuilder.DropTable("Orders");
        }
    }
}
