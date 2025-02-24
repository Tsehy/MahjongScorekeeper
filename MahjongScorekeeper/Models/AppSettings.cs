using System;
using System.Collections.Generic;

namespace MahjongScorekeeper.Models;

public class AppSettings
{
    public string CurrentSeason { get; set; } = DateTime.Today.Year.ToString();
    public List<string> Players { get; set; } = [];

    public void SetTo(AppSettings? other)
    {
        if (other != null)
        {
            CurrentSeason = other.CurrentSeason;
            Players = [.. other.Players];
        }
    }
}
