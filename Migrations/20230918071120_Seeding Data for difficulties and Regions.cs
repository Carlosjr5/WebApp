using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDatafordifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9d4ee3b0-6315-45d7-8ce1-a118655a5eb8"), "Medium" },
                    { new Guid("b27fdc88-793c-45df-ae6e-d9cf9a655623"), "Easy" },
                    { new Guid("df7dd721-b4ad-41e1-89d4-6f61f02c7e98"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("13b89376-ac4f-4276-8c57-0b35004f1bc3"), "SEV", "Sevilla", "https://www.pexels.com/photo/people-walking-near-the-water-fountain-5470587/" },
                    { new Guid("3a5d6e12-9f67-4d9e-8a9c-1b2c567d8901"), "BCN", "Barcelona", "https://www.pexels.com/photo/beautiful-cityscape-of-barcelona-260330/" },
                    { new Guid("7eae83d0-1e55-4e4d-b8ad-2c56e18b9f8f"), "PAR", "Paris", "https://www.pexels.com/photo/eiffel-tower-338515/" },
                    { new Guid("f4e98e53-6f67-4a3b-ae27-9e8b6d8c6a5f"), "NYC", "New York City", "https://www.pexels.com/photo/time-square-1367277/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9d4ee3b0-6315-45d7-8ce1-a118655a5eb8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b27fdc88-793c-45df-ae6e-d9cf9a655623"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("df7dd721-b4ad-41e1-89d4-6f61f02c7e98"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("13b89376-ac4f-4276-8c57-0b35004f1bc3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3a5d6e12-9f67-4d9e-8a9c-1b2c567d8901"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7eae83d0-1e55-4e4d-b8ad-2c56e18b9f8f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f4e98e53-6f67-4a3b-ae27-9e8b6d8c6a5f"));
        }
    }
}
