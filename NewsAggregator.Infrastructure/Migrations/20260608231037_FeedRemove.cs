using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeedRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Articles_ArticlesId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Categories_CategoriesId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Feeds_FeedId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Articles_FeedId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "ArticleCategory",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ArticlesId",
                table: "ArticleCategory",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategory_CategoriesId",
                table: "ArticleCategory",
                newName: "IX_ArticleCategory_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUrl",
                table: "Sources",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Articles",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000);

            migrationBuilder.CreateTable(
                name: "UserArticleLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticleLikes_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticleLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCategoryPreferences",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryPreferences", x => new { x.UserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategoryPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleLikes_ArticleId",
                table: "UserArticleLikes",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleLikes_UserId_ArticleId",
                table: "UserArticleLikes",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryPreferences_CategoryId",
                table: "UserCategoryPreferences",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Articles_ArticleId",
                table: "ArticleCategory",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Articles_ArticleId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory");

            migrationBuilder.DropTable(
                name: "UserArticleLikes");

            migrationBuilder.DropTable(
                name: "UserCategoryPreferences");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ArticleCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "ArticleCategory",
                newName: "ArticlesId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategory_CategoryId",
                table: "ArticleCategory",
                newName: "IX_ArticleCategory_CategoriesId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<string>(
                name: "BaseUrl",
                table: "Sources",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Articles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AddColumn<Guid>(
                name: "FeedId",
                table: "Articles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FeedId",
                table: "Articles",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_SourceId",
                table: "Feeds",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Articles_ArticlesId",
                table: "ArticleCategory",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Categories_CategoriesId",
                table: "ArticleCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Feeds_FeedId",
                table: "Articles",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id");
        }
    }
}
