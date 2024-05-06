using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace register_auth_cokikes.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role2",
                table: "AuthUser",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AuthUser",
                newName: "Role2");
        }
    }
}
