using Application.ViewModels.BattleshipGame;
using Application.ViewModels.ShipPosition;
using Domain.Common.Enums;

namespace Application.ViewModels.Ship;

public class ShipViewModel
{
    public Guid Id { get; set; }
    
    // Relación con juego y jugador
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    
    // Tipo y tamaño del barco
    public ShipType Type { get; set; }
    public int Size { get; set; }
    
    // Posición y dirección
    public int StartX { get; set; }
    public int StartY { get; set; }
    public Direction Direction { get; set; }
    
    // Estado
    public bool IsSunk { get; set; } = false;
    
    // Relaciones
    public BattleshipGameViewModel Game { get; set; }
    public ICollection<ShipPositionViewModel> Positions { get; set; }
}
