using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityEmail.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_SentMessage_Priority_and_Category_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SentMessages",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "SentMessages",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "SentMessages");
        }
    }
}
