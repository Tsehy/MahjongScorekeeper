using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Factories;
using MahjongScorekeeper.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MahjongScorekeeper.ViewModels;

public partial class PlayedGamesPageViewModel : PageViewModel
{
    private readonly DialogFactory _dialogFactory;

    public GameCollection GameCollection { get; }

    public IEnumerable<string> Players => GameCollection.Games.SelectMany(g => g.Data.Select(d => d.Name)).Distinct();

    public ObservableCollection<ExtendedGame> ExtendedGames { get; }

    public PlayedGamesPageViewModel(
        GameCollection gamesColl,
        DialogFactory factory
    )
    {
        PageType = PageViewType.PlayedGames;

        GameCollection = gamesColl;
        _dialogFactory = factory;

        var p = Players.ToList(); // don't call it over and over...
        ExtendedGames = new(GameCollection.Games.Select(g => new ExtendedGame(g, p)));
    }

    [RelayCommand]
    public async Task AddGame(Window parentWingow)
    {
        if (parentWingow != null)
        {
            Game result = await _dialogFactory.GetDialogView(DialogViewType.AddGame).ShowDialog<Game>(parentWingow);
            if (result != null)
            {
                GameCollection.Games.Add(result);
                ExtendedGames.Add(new(result, Players));
                // TODO if there's a new player, extend the DataGrid!
            }
        }
    }
}
