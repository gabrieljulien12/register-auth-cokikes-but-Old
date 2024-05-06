using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace register_auth_cokikes.Migrations
{
    /// <inheritdoc />
    public partial class uptade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthUser");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AuthUser",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthUser");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AuthUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "user");
        }
    }
}
