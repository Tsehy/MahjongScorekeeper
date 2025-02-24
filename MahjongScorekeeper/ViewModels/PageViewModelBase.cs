using CommunityToolkit.Mvvm.ComponentModel;
using MahjongScorekeeper.Data;

namespace MahjongScorekeeper.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private PageViewType _pageType;
}
