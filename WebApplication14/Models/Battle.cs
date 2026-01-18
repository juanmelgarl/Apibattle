using System;
using System.Collections.Generic;

namespace WebApplication14.Models;

public partial class Battle
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

    public virtual ICollection<BattleCommander> BattleCommanders { get; set; } = new List<BattleCommander>();

    public virtual ICollection<BattleHouse> BattleHouses { get; set; } = new List<BattleHouse>();

    public virtual ICollection<BattleKing> BattleKings { get; set; } = new List<BattleKing>();
}
