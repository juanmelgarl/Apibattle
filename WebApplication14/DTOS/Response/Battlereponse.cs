namespace WebApplication14.DTOS.Response
{
    public class Battlereponse
    {
        public int Id { get; set; }

        public int? BattleTypeId { get; set; }

        public int? LocationId { get; set; }

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
