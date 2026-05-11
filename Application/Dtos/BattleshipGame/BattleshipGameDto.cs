using Application.Dtos.Attack;
using Application.Dtos.Ship;
using Application.Dtos.User;
using Domain.Common.Enums;
namespace Application.Dtos.BattleshipGame;

public class BattleshipGameDto
{
    public Guid Id { get; set; }
    
    // Jugadores
    public required Guid Player1Id { get; set; }
    public required Guid Player2Id { get; set; }
    
    // Estado del juego
    public GameStatus Status { get; set; } = GameStatus.SettingUp;
    public required Guid CurrentTurnPlayerId { get; set; }
    public Guid WinnerId { get; set; }
    
    // Tiempos
    public required DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public DateTime LastMoveDate { get; set; } = DateTime.Now;
    
    // Relaciones
    public UserDto? Player1 { get; set; } // se carga en el servicio
    public UserDto? Player2 { get; set; }
    public ICollection<ShipDto> Ships { get; set; }
    public ICollection<AttackDto> Attacks { get; set; }
}