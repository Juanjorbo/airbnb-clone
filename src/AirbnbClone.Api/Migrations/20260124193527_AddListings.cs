using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirbnbClone.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PricePerNight = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxGuests = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listing_City",
                table: "Listing",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_PricePerNight",
                table: "Listing",
                column: "PricePerNight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listing");
        }
    }
}
