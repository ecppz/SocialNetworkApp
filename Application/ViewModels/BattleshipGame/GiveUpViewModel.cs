namespace Application.ViewModels.BattleshipGame;

public class GiveUpViewModel
{
    public required Guid GameId { get; set; }
    public required Guid UserId { get; set; }
    public required bool RedirectToHome { get; set; }
}