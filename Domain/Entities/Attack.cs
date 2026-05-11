namespace Domain.Entities;

public class Attack
{
    public Guid Id { get; set; }
    
    // Relación con juego y atacante
    public Guid GameId { get; set; }
    public Guid AttackerId { get; set; }
    
    // Coordenadas del ataque
    public int X { get; set; }
    public int Y { get; set; }
    
    // Resultado
    public bool IsHit { get; set; }
    public DateTime AttackTime { get; set; } = DateTime.Now;
    
    // Relaciones
    public BattleshipGame Game { get; set; }
}
