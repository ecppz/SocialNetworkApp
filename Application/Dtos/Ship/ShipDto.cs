using Application.Dtos.BattleshipGame;
using Application.Dtos.ShipPosition;
using Domain.Common.Enums;

namespace Application.Dtos.Ship;

public class ShipDto
{
    public Guid Id { get; set; }
    
    // Relación con juego y jugador
    public required Guid GameId { get; set; }
    public required Guid PlayerId { get; set; }
    
    // Tipo y tamaño del barco
    public required ShipType Type { get; set; }
    public required int Size { get; set; }
    
    // Posición y dirección
    public required int StartX { get; set; }
    public required int StartY { get; set; }
    public required Direction Direction { get; set; }
    
    // Estado
    public bool IsSunk { get; set; } = false;
    
    // Relaciones
    public BattleshipGameDto Game { get; set; }
    public ICollection<ShipPositionDto> Positions { get; set; }
}
