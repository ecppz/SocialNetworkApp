using Domain.Common.Enums;

namespace Domain.Entities;


public class BattleshipGame
{
    public Guid Id { get; set; }
    
    // Jugadores
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    
    // Estado del juego
    public GameStatus Status { get; set; } = GameStatus.SettingUp;
    public Guid CurrentTurnPlayerId { get; set; }
    public Guid? WinnerId { get; set; }
    
    // Tiempos
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public DateTime LastMoveDate { get; set; } = DateTime.Now;
    
    // Relaciones
    public ICollection<Ship> Ships { get; set; }
    public ICollection<Attack> Attacks { get; set; }
}