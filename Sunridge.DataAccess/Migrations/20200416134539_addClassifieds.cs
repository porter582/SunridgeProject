using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class addClassifieds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            

            migrationBuilder.CreateTable(
                name: "ClassifiedCategory",
                columns: table => new
                {
                    ClassifiedCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedCategory", x => x.ClassifiedCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedService", x => x.Id);
                });



            migrationBuilder.CreateTable(
                name: "ClassifiedListing",
                columns: table => new
                {
                    ClassifiedListingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(nullable: true),
                    ClassifiedCategoryId = table.Column<int>(nullable: false),
                    Categories = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(maxLength: 75, nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    ListingDate = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    classifiedcategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedListing", x => x.ClassifiedListingId);
                    table.ForeignKey(
                        name: "FK_ClassifiedListing_ClassifiedCategory_ClassifiedCategoryId",
                        column: x => x.ClassifiedCategoryId,
                        principalTable: "ClassifiedCategory",
                        principalColumn: "ClassifiedCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassifiedListing_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

          

            migrationBuilder.CreateTable(
                name: "ClassifiedImage",
                columns: table => new
                {
                    ClassifiedImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassifiedListingId = table.Column<int>(nullable: false),
                    IsMainImage = table.Column<bool>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    ImageExtension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedImage", x => x.ClassifiedImageId);
                    table.ForeignKey(
                        name: "FK_ClassifiedImage_ClassifiedListing_ClassifiedListingId",
                        column: x => x.ClassifiedListingId,
                        principalTable: "ClassifiedListing",
                        principalColumn: "ClassifiedListingId",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedImage_ClassifiedListingId",
                table: "ClassifiedImage",
                column: "ClassifiedListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedListing_ClassifiedCategoryId",
                table: "ClassifiedListing",
                column: "ClassifiedCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedListing_OwnerId",
                table: "ClassifiedListing",
                column: "OwnerId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ClassifiedImage");

            migrationBuilder.DropTable(
                name: "ClassifiedService");


            migrationBuilder.DropTable(
                name: "ClassifiedListing");

           
            migrationBuilder.DropTable(
                name: "ClassifiedCategory");

        }
    }
}
