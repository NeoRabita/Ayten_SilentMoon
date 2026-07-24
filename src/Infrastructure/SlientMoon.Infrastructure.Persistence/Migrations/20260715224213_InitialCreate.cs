using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlientMoon.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Token = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Expires = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatedByIp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PasswordHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    OtpCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    OtpExpireDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    OtpAttemptCount = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RefreshTokenId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_RefreshTokens_RefreshTokenId",
                        column: x => x.RefreshTokenId,
                        principalTable: "RefreshTokens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pomodoro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    POMODORO_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PomodoroTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ShortBreakTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LongBreakTime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LongBreakInterval = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PeriodCount = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Color = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UserId1 = table.Column<int>(type: "NUMBER(10)", nullable: true)
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
                name: "IX_ApplicationUsers_RefreshTokenId",
                table: "ApplicationUsers",
                column: "RefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Pomodoro_UserId1",
                table: "Pomodoro",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pomodoro");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
