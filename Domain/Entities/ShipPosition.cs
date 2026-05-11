
namespace Domain.Entities;

public class ShipPosition
{
    public Guid Id { get; set; }
    public Guid ShipId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsHit { get; set; } = false;
    
    // Relaciones
    public Ship Ship { get; set; }
}
