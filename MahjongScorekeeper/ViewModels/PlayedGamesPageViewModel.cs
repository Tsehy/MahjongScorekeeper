using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Factories;
using MahjongScorekeeper.Models;
using MahjongScorekeeper.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MahjongScorekeeper.ViewModels;

public partial class PlayedGamesPageViewModel : PageViewModel
{
    private readonly DialogFactory _dialogFactory;

    [ObservableProperty]
    private GameCollection _gameCollection;

    public ObservableCollection<string> Players => new(GameCollection.Games.SelectMany(g => g.Data.Select(d => d.Name)).Distinct());

    public ObservableCollection<ExtendedGame> ExtendedGamesList { get; }

    public PlayedGamesPageViewModel(
        GameCollection gamesColl,
        DialogFactory factory
    )
    {
        PageType = PageViewType.PlayedGames;

        GameCollection = gamesColl;
        _dialogFactory = factory;

        var p = Players.ToList();
        ExtendedGamesList = new(GameCollection.Games.Select(g => new ExtendedGame(g, p)));
    }

    [RelayCommand]
    public void AddGame(Window parentWingow)
    {
        if (parentWingow != null)
        {
            _dialogFactory.GetDialogView(DialogViewType.AddGame).ShowDialog(parentWingow);
        }
    }
}
