using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlientMoon.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Subtitle = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Category = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Duration = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DurationSeconds = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsFeatured = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsDailyThought = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsRecommended = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsPopular = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    SortOrder = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentNarrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ContentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Gender = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentNarrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentNarrators_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentTopics",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TopicId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTopics", x => new { x.ContentId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_ContentTopics_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentNarrators_ContentId",
                table: "ContentNarrators",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTopics_TopicId",
                table: "ContentTopics",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentNarrators");

            migrationBuilder.DropTable(
                name: "ContentTopics");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
