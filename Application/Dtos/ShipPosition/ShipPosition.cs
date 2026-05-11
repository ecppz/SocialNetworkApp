using Application.Dtos.Ship;

namespace Application.Dtos.ShipPosition;

public class ShipPositionDto
{
    public Guid Id { get; set; }
    public Guid ShipId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsHit { get; set; } = false;
    
    // Relaciones
    public ShipDto Ship { get; set; }
}
