using CommunityToolkit.Mvvm.ComponentModel;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Models;

namespace MahjongScorekeeper.ViewModels;

public partial class PlayersPageViewModel : PageViewModel
{
    [ObservableProperty]
    private AppSettings _settings;

    public string Test => "Players test ikszdé";

    // TODO Linq to get players
    // ToDoList like UI

    public PlayersPageViewModel(AppSettings settings)
    {
        Settings = settings;
        PageType = PageViewType.Players;
    }
}
