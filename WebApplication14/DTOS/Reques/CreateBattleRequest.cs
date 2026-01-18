namespace WebApplication14.DTOS.Reques
{
    public class CreateBattleRequest
    {
        public string Name { get; set; } = null!;

        public int Year { get; set; }

        public bool AttackerWon { get; set; }

        public string? Notes { get; set; }

        public bool HasMayorCapture { get; set; }

        public bool HasMayorDeath { get; set; }

        public int AmountAttackerSoldiers { get; set; }

        public int AmountDefenderSoldiers { get; set; }
    }
}
