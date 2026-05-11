using Application.ViewModels.Attack;
using Application.ViewModels.Ship;
using Application.ViewModels.User;
using Domain.Common.Enums;


namespace Application.ViewModels.BattleshipGame;


public class BattleshipGameViewModel
{
    public Guid Id { get; set; }
    
    // Jugadores
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    
    // Estado del juego
    public GameStatus Status { get; set; } = GameStatus.SettingUp;
    public Guid CurrentTurnPlayerId { get; set; }
    public Guid WinnerId { get; set; }
    
    // Tiempos
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public DateTime LastMoveDate { get; set; } = DateTime.Now;
    
    // Relaciones
    public UserViewModel? Player1 { get; set; } // se carga en el servicio
    public UserViewModel? Player2 { get; set; }
    public ICollection<ShipViewModel> Ships { get; set; }
    public ICollection<AttackViewModel> Attacks { get; set; }
}