using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OverwatchApplication.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    HeroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyToMaster = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeaponType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.HeroID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    AbilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Cooldown = table.Column<float>(type: "real", nullable: false),
                    HeroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.AbilityID);
                    table.ForeignKey(
                        name: "FK_Abilities_Heroes_HeroID",
                        column: x => x.HeroID,
                        principalTable: "Heroes",
                        principalColumn: "HeroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroNotes",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoursPlayed = table.Column<float>(type: "real", nullable: false),
                    HeroID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroNotes", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_HeroNotes_Heroes_HeroID",
                        column: x => x.HeroID,
                        principalTable: "Heroes",
                        principalColumn: "HeroID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroNotes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Heroes",
                columns: new[] { "HeroID", "CountryOfOrigin", "Description", "DifficultyToMaster", "Gender", "ImageURL", "Name", "Role", "WeaponType" },
                values: new object[,]
                {
                    { 1, "United Kingdom", "Time-jumping, high-mobility damage hero.", 3, "Female", "/images/tracer.png", "Tracer", 1, "Pulse Pistols" },
                    { 2, "Germany", "Armored knight with a massive barrier shield.", 2, "Male", "/images/reinhardt.png", "Reinhardt", 0, "Rocket Hammer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "PasswordHash", "UserName" },
                values: new object[] { 1, "playerone@example.com", "hashedpassword123", "PlayerOne" });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "AbilityID", "Cooldown", "Description", "HeroID", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 0f, "Twin automatic pistols with rapid fire.", 1, "Pulse Pistols", 0 },
                    { 2, 3f, "Teleport a short distance in the direction you're moving.", 1, "Blink", 2 },
                    { 3, 0f, "A large melee weapon that deals sweeping melee damage.", 2, "Rocket Hammer", 0 },
                    { 4, 0f, "Hold a large energy shield to protect allies.", 2, "Barrier Field", 2 }
                });

            migrationBuilder.InsertData(
                table: "HeroNotes",
                columns: new[] { "NoteID", "Content", "DateCreated", "DateModified", "HeroID", "HoursPlayed", "UserID" },
                values: new object[,]
                {
                    { 1, "Practice aiming while blinking.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 5f, 1 },
                    { 2, "Use Barrier Field to protect the team when pushing choke points.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 3f, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_HeroID",
                table: "Abilities",
                column: "HeroID");

            migrationBuilder.CreateIndex(
                name: "IX_HeroNotes_HeroID",
                table: "HeroNotes",
                column: "HeroID");

            migrationBuilder.CreateIndex(
                name: "IX_HeroNotes_UserID",
                table: "HeroNotes",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "HeroNotes");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
