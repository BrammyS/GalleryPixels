using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalleryPixels.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialUserProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsInitialUser",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsInitialUser",
                table: "AspNetUsers");
        }
    }
}
