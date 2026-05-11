using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;
namespace Application.ViewModels.BattleshipGame;

public class ShipSelectionViewModel
{
    public required Guid GameId { get; set; }
    public List<ShipType> MissingShips { get; set; } = [];
    
    [Required(ErrorMessage = "Debes de seleccionar un barco por lo menos")]
    [Range(2, 5, ErrorMessage = "debes de seleccionar un barco por lo menos")]
    public int SelectedShip { get; set; }
}