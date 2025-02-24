using CommunityToolkit.Mvvm.ComponentModel;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Models;

namespace MahjongScorekeeper.ViewModels;

public partial class SeasonPageViewModel : PageViewModel
{
    [ObservableProperty]
    private AppSettings _settings;

    public string Test => "Rabbit season";

    // TODO load available json names for switching!

    public SeasonPageViewModel(AppSettings settings)
    {
        Settings = settings;
        PageType = PageViewType.Season;
    }
}
