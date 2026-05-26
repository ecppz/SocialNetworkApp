using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattleshipGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Player1Id = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: false),
                    Player2Id = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CurrentTurnPlayerId = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastMoveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleshipGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageFile = table.Column<string>(type: "text", nullable: true),
                    YouTubeUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttackerId = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    IsHit = table.Column<bool>(type: "boolean", nullable: false),
                    AttackTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_BattleshipGames_GameId",
                        column: x => x.GameId,
                        principalTable: "BattleshipGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", maxLength: 450, nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    StartX = table.Column<int>(type: "integer", nullable: false),
                    StartY = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsSunk = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_BattleshipGames_GameId",
                        column: x => x.GameId,
                        principalTable: "BattleshipGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplyToCommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ReplyToCommentId",
                        column: x => x.ReplyToCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShipId = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    IsHit = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipPositions_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_AttackTime",
                table: "Attacks",
                column: "AttackTime");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId_AttackerId",
                table: "Attacks",
                columns: new[] { "GameId", "AttackerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId_AttackerId_X_Y",
                table: "Attacks",
                columns: new[] { "GameId", "AttackerId", "X", "Y" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId_X_Y",
                table: "Attacks",
                columns: new[] { "GameId", "X", "Y" });

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_CurrentTurnPlayerId",
                table: "BattleshipGames",
                column: "CurrentTurnPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_LastMoveDate",
                table: "BattleshipGames",
                column: "LastMoveDate");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_Player1Id",
                table: "BattleshipGames",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_Player2Id",
                table: "BattleshipGames",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_Status",
                table: "BattleshipGames",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyToCommentId",
                table: "Comments",
                column: "ReplyToCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderUserId_ReceiverUserId",
                table: "FriendRequests",
                columns: new[] { "SenderUserId", "ReceiverUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_PostId",
                table: "Reactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipPositions_ShipId_IsHit",
                table: "ShipPositions",
                columns: new[] { "ShipId", "IsHit" });

            migrationBuilder.CreateIndex(
                name: "IX_ShipPositions_ShipId_X_Y",
                table: "ShipPositions",
                columns: new[] { "ShipId", "X", "Y" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipPositions_X_Y",
                table: "ShipPositions",
                columns: new[] { "X", "Y" });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_GameId_PlayerId",
                table: "Ships",
                columns: new[] { "GameId", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_IsSunk",
                table: "Ships",
                column: "IsSunk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "ShipPositions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "BattleshipGames");
        }
    }
}
