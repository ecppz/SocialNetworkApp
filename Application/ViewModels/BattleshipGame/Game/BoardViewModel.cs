
using Application.ViewModels.BattleshipGame.Game.Enums;
using Domain.Common.Enums;

namespace Application.ViewModels.BattleshipGame.Game;


public class BoardViewModel
{
    public required Guid GameId { get; set; }
    public required BoardType Type { get; set; } // Ataque, Posicionamiento
    public required bool IsMyTurn { get; set; }
    
    // Matriz de celdas
    public List<List<CellViewModel>> Cells { get; set; } = new List<List<CellViewModel>>();
    
    // Información adicional según el tipo de tablero
    public ShipType? SelectedShip { get; set; } // Solo para posicionamiento
}