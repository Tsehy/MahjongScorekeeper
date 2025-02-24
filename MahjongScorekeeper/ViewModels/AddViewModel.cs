using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MahjongScorekeeper.ViewModels;

public partial class AddViewModel : ViewModelBase
{
    private const int BASESCORE = 25000;
    private readonly int[] UMA = { 15, 5, -5, -15 };

    [ObservableProperty]
    private GameCollection _gameCollection;
    [ObservableProperty]
    private AppSettings _appSettings;

    public ObservableCollection<string> Players => new(AppSettings.Players);
    public ObservableCollection<GameType> GameTypes => new(Enum.GetValues(typeof(GameType)).Cast<GameType>());

    [ObservableProperty]
    private DateTime _gameDate = DateTime.Today;
    [ObservableProperty]
    private GameType _type = GameType.Hanchan;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _eastName;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _southName;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _westName;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _northName;

    // TODO accept only numbers!
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private double _eastScore;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private double _southScore;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private double _westScore;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private double _northScore;

    public AddViewModel(
        GameCollection collection,
        AppSettings settings
    )
    {
        GameCollection = collection;
        AppSettings = settings;
    }

    [RelayCommand]
    public void CloseDialog(Window dialogWindow) => dialogWindow.Close(); // don't trust intellisense and never make a command static!

    private bool IsValid()
    {
        if (EastScore + SouthScore + WestScore + NorthScore != 4 * BASESCORE)
        {
            return false;
        }

        string[] names = [EastName, SouthName, WestName, NorthName];

        return !names.Any(string.IsNullOrWhiteSpace) // all name has value
            && names.Distinct().Count() == 4;        // all name is different
    }

    [RelayCommand(CanExecute = nameof(IsValid))]
    public void Save(Window dialogWindow)
    {
        GameData[] data = [
            new(EastName, EastScore),
            new(SouthName, SouthScore),
            new(WestName, WestScore),
            new(NorthName, NorthScore)
        ];

        GameData[] tournamentData = data.ToTournamentScore(BASESCORE, UMA);

        GameCollection.Games.Add(new(
            DateOnly.FromDateTime(GameDate),
            Type,
            tournamentData
        ));
        CloseDialog(dialogWindow);
    }
}
