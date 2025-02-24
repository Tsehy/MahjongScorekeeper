using MahjongScorekeeper.Data;
using MahjongScorekeeper.ViewModels;
using System;

namespace MahjongScorekeeper.Factories;

public class PageFactory(Func<PageViewType, PageViewModel> factory)
{
    public PageViewModel GetPageView(PageViewType pageViewType) => factory.Invoke(pageViewType);
}
