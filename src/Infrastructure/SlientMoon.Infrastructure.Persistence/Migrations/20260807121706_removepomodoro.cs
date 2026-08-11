using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SlientMoon.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removepomodoro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pomodoro");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "ApplicationUsers",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUsers",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Time = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    Days = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ImageUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ApplicationUserId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTopics",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TopicId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopics", x => new { x.UserId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_UserTopics_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "ApplicationUserId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, null, "stress.png", "Reduce Stress" },
                    { 2, null, "performance.png", "Improve Performance" },
                    { 3, null, "anxiety.png", "Reduce Anxiety" },
                    { 4, null, "happiness.png", "Increase Happiness" },
                    { 5, null, "growth.png", "Personal Growth" },
                    { 6, null, "sleep.png", "Better Sleep" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ApplicationUserId",
                table: "Topics",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopics_TopicId",
                table: "UserTopics",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "UserTopics");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUsers");

            migrationBuilder.CreateTable(
                name: "Pomodoro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId1 = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Color = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    LongBreakInterval = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LongBreakTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    POMODORO_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PeriodCount = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PomodoroTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ShortBreakTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pomodoro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pomodoro_ApplicationUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pomodoro_UserId1",
                table: "Pomodoro",
                column: "UserId1");
        }
    }
}
