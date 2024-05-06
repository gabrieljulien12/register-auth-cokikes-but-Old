using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace register_auth_cokikes.Migrations
{
    /// <inheritdoc />
    public partial class UserUptade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AuthUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthUser");
        }
    }
}
