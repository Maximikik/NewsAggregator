using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnArticleUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_Url",
                table: "Articles",
                column: "Url",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_Url",
                table: "Articles");
        }
    }
}
