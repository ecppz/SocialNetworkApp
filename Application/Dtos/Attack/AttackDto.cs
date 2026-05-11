using Application.Dtos.BattleshipGame;

namespace Application.Dtos.Attack;

public class AttackDto
{
    public Guid Id { get; set; }
    
    // Relación con juego y atacante
    public required Guid GameId { get; set; }
    public required Guid AttackerId { get; set; }
    
    // Coordenadas del ataque
    public required int X { get; set; }
    public required int Y { get; set; }
    
    // Resultado
    public bool IsHit { get; set; }
    public DateTime AttackTime { get; set; } = DateTime.Now;
    
    // Relaciones
    public BattleshipGameDto Game { get; set; }
}
