using CommunityToolkit.Mvvm.ComponentModel;
using MahjongScorekeeper.Models;
using System;

namespace MahjongScorekeeper.ViewModels;

public partial class StatisticsPageViewModel : PageViewModel
{
    [ObservableProperty]
    private GameCollection _games;

    public string Test => "Statistics string lmao";

    // TODO Linq magic to get the statistics

    public StatisticsPageViewModel(
        GameCollection games
    )
    {
        Games = games;
        //Games.Games.Add(new Game()
        //{
        //    Date = new DateTime(2025, 1, 31),
        //    Type = Data.GameType.Hanchan,
        //    Data = new[] {
        //        new GameData() { Name = "Niki", Score = -28.5 },
        //        new GameData() { Name = "Csehi", Score = 8.2 },
        //        new GameData() { Name = "Dani", Score = 27.7 },
        //        new GameData() { Name = "Boi", Score = -7.4 },
        //    }
        //});
        //Games.Games.Add(new Game()
        //{
        //    Date = new DateTime(2025, 1, 31),
        //    Type = Data.GameType.Tonpuusen,
        //    Data = new[] {
        //        new GameData() { Name = "Niki", Score = 9.5 },
        //        new GameData() { Name = "Csehi", Score = -22.7 },
        //        new GameData() { Name = "Dani", Score = -7.5 },
        //        new GameData() { Name = "Boi", Score = 20.7 },
        //    }
        //});
        //Games.Games.Add(new Game()
        //{
        //    Date = new DateTime(2025, 2, 14),
        //    Type = Data.GameType.Hanchan,
        //    Data = new[] {
        //        new GameData() { Name = "Niki", Score = 11.9 },
        //        new GameData() { Name = "Csehi", Score = 26.4 },
        //        new GameData() { Name = "Dani", Score = -29.2 },
        //        new GameData() { Name = "Deák", Score = -9.1 },
        //    }
        //});
        PageType = Data.PageViewType.Statistics;
    }
}
