using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Factories;
using MahjongScorekeeper.Models;

namespace MahjongScorekeeper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty]
    private AppSettings _settings;

    [ObservableProperty]
    private PageViewModel _currentPage;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoToStatisticsCommand))]
    [NotifyCanExecuteChangedFor(nameof(GoToPlayedGamesCommand))]
    [NotifyCanExecuteChangedFor(nameof(GoToPlayersCommand))]
    [NotifyCanExecuteChangedFor(nameof(GoToSeasonCommand))]
    private PageViewType _currentPageType;

    public MainViewModel(
        PageFactory factory,
        AppSettings settings
    )
    {
        _pageFactory = factory;
        Settings = settings;

        CurrentPage = _pageFactory.GetPageView(PageViewType.Statistics);
        CurrentPageType = CurrentPage.PageType;
    }

    private bool NotStatistics() => CurrentPageType != PageViewType.Statistics;
    private bool NotPlayedGames() => CurrentPageType != PageViewType.PlayedGames;
    private bool NotPlayers() => CurrentPageType != PageViewType.Players;
    private bool NotSeason() => CurrentPageType != PageViewType.Season;

    [RelayCommand(CanExecute = nameof(NotStatistics))]
    public void GoToStatistics()
    {
        CurrentPage = _pageFactory.GetPageView(PageViewType.Statistics);
        CurrentPageType = CurrentPage.PageType;
    }

    [RelayCommand(CanExecute = nameof(NotPlayedGames))]
    public void GoToPlayedGames()
    {
        CurrentPage = _pageFactory.GetPageView(PageViewType.PlayedGames);
        CurrentPageType = CurrentPage.PageType;
    }

    [RelayCommand(CanExecute = nameof(NotPlayers))]
    public void GoToPlayers()
    {
        CurrentPage = _pageFactory.GetPageView(PageViewType.Players);
        CurrentPageType = CurrentPage.PageType;
    }

    [RelayCommand(CanExecute = nameof(NotSeason))]
    public void GoToSeason()
    {
        CurrentPage = _pageFactory.GetPageView(PageViewType.Season);
        CurrentPageType = CurrentPage.PageType;
    }
}
