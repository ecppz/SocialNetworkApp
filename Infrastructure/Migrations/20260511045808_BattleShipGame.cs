using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BattleShipGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear tabla BattleshipGames
            migrationBuilder.CreateTable(
                name: "BattleshipGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CurrentTurnPlayerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    LastMoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleshipGames", x => x.Id);
                });

            // Crear tabla Ships
            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    StartX = table.Column<int>(type: "int", nullable: false),
                    StartY = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsSunk = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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

            // Crear tabla ShipPositions
            migrationBuilder.CreateTable(
                name: "ShipPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    IsHit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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

            // Crear tabla Attacks
            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttackTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    IsHit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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

            // Índices
            migrationBuilder.CreateIndex(
                name: "IX_Ships_GameId_PlayerId",
                table: "Ships",
                columns: new[] { "GameId", "PlayerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_IsSunk",
                table: "Ships",
                column: "IsSunk");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_CurrentTurnPlayerId",
                table: "BattleshipGames",
                column: "CurrentTurnPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_LastMoveDate",
                table: "BattleshipGames",
                column: "LastMoveDate");

            migrationBuilder.CreateIndex(
                name: "IX_BattleshipGames_Status",
                table: "BattleshipGames",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId_AttackerId_X_Y",
                table: "Attacks",
                columns: new[] { "GameId", "AttackerId", "X", "Y" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipPositions_ShipId_X_Y",
                table: "ShipPositions",
                columns: new[] { "ShipId", "X", "Y" },
                unique: true);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_BattleshipGames_GameId",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "ShipPositions");

            migrationBuilder.DropIndex(
                name: "IX_Ships_GameId_PlayerId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_IsSunk",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleshipGames",
                table: "BattleshipGames");

            migrationBuilder.DropIndex(
                name: "IX_BattleshipGames_CurrentTurnPlayerId",
                table: "BattleshipGames");

            migrationBuilder.DropIndex(
                name: "IX_BattleshipGames_LastMoveDate",
                table: "BattleshipGames");

            migrationBuilder.DropIndex(
                name: "IX_BattleshipGames_Status",
                table: "BattleshipGames");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "StartX",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "CurrentTurnPlayerId",
                table: "BattleshipGames");

            migrationBuilder.DropColumn(
                name: "LastMoveDate",
                table: "BattleshipGames");

            migrationBuilder.RenameTable(
                name: "BattleshipGames",
                newName: "BattleShipGames");

            migrationBuilder.RenameColumn(
                name: "StartY",
                table: "Ships",
                newName: "Orientation");

            migrationBuilder.RenameIndex(
                name: "IX_BattleshipGames_Player2Id",
                table: "BattleShipGames",
                newName: "IX_BattleShipGames_Player2Id");

            migrationBuilder.RenameIndex(
                name: "IX_BattleshipGames_Player1Id",
                table: "BattleShipGames",
                newName: "IX_BattleShipGames_Player1Id");

            migrationBuilder.RenameColumn(
                name: "Y",
                table: "Attacks",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "AttackTime",
                table: "Attacks",
                newName: "AttackDate");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Ships",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSunk",
                table: "Ships",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InitialCoordinate",
                table: "Ships",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Ships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BattleShipGames",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentTurnUserId",
                table: "BattleShipGames",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "DurationHours",
                table: "BattleShipGames",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinate",
                table: "Attacks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleShipGames",
                table: "BattleShipGames",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ShipCells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Coordinate = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IsHit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipCells_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_GameId_UserId",
                table: "Ships",
                columns: new[] { "GameId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId",
                table: "Attacks",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_GameId_AttackerId_Coordinate",
                table: "Attacks",
                columns: new[] { "GameId", "AttackerId", "Coordinate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipCells_ShipId_Coordinate",
                table: "ShipCells",
                columns: new[] { "ShipId", "Coordinate" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_BattleShipGames_GameId",
                table: "Attacks",
                column: "GameId",
                principalTable: "BattleShipGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_BattleShipGames_GameId",
                table: "Ships",
                column: "GameId",
                principalTable: "BattleShipGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
