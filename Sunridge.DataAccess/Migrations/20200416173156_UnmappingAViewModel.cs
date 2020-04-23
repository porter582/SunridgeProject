using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunridge.DataAccess.Migrations
{
    public partial class UnmappingAViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUsers_ClassifiedListingViewModel_ClassifiedListingViewModelId",
            //    table: "AspNetUsers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ClassifiedCategory_ClassifiedListingViewModel_ClassifiedListingViewModelId",
            //    table: "ClassifiedCategory");

            //migrationBuilder.DropTable(
            //    name: "ClassifiedListingViewModel");

            //migrationBuilder.DropIndex(
            //    name: "IX_ClassifiedCategory_ClassifiedListingViewModelId",
            //    table: "ClassifiedCategory");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUsers_ClassifiedListingViewModelId",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "ClassifiedListingViewModelId",
            //    table: "ClassifiedCategory");

            //migrationBuilder.DropColumn(
            //    name: "ClassifiedListingViewModelId",
            //    table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassifiedListingViewModelId",
                table: "ClassifiedCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassifiedListingViewModelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassifiedListingViewModel",
                columns: table => new
                {
                    ClassifiedListingViewModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassifiedListingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedListingViewModel", x => x.ClassifiedListingViewModelId);
                    table.ForeignKey(
                        name: "FK_ClassifiedListingViewModel_ClassifiedListing_ClassifiedListingId",
                        column: x => x.ClassifiedListingId,
                        principalTable: "ClassifiedListing",
                        principalColumn: "ClassifiedListingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedCategory_ClassifiedListingViewModelId",
                table: "ClassifiedCategory",
                column: "ClassifiedListingViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassifiedListingViewModelId",
                table: "AspNetUsers",
                column: "ClassifiedListingViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedListingViewModel_ClassifiedListingId",
                table: "ClassifiedListingViewModel",
                column: "ClassifiedListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClassifiedListingViewModel_ClassifiedListingViewModelId",
                table: "AspNetUsers",
                column: "ClassifiedListingViewModelId",
                principalTable: "ClassifiedListingViewModel",
                principalColumn: "ClassifiedListingViewModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassifiedCategory_ClassifiedListingViewModel_ClassifiedListingViewModelId",
                table: "ClassifiedCategory",
                column: "ClassifiedListingViewModelId",
                principalTable: "ClassifiedListingViewModel",
                principalColumn: "ClassifiedListingViewModelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
