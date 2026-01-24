using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirbnbClone.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameListingTableToListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Listing",
                table: "Listing");

            migrationBuilder.RenameTable(
                name: "Listing",
                newName: "Listings");

            migrationBuilder.RenameIndex(
                name: "IX_Listing_PricePerNight",
                table: "Listings",
                newName: "IX_Listings_PricePerNight");

            migrationBuilder.RenameIndex(
                name: "IX_Listing_City",
                table: "Listings",
                newName: "IX_Listings_City");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listings",
                table: "Listings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Listings",
                table: "Listings");

            migrationBuilder.RenameTable(
                name: "Listings",
                newName: "Listing");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_PricePerNight",
                table: "Listing",
                newName: "IX_Listing_PricePerNight");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_City",
                table: "Listing",
                newName: "IX_Listing_City");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Listing",
                table: "Listing",
                column: "Id");
        }
    }
}
