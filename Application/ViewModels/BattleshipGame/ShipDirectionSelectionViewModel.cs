using Domain.Common.Enums;

namespace Application.ViewModels.BattleshipGame;

public class ShipDirectionSelectionViewModel
{
    public required int GameId { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
    public ShipType SelectedShip { get; set; }
    public Direction SelectedDirection { get; set; }
}