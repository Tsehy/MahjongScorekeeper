using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using MahjongScorekeeper.Models;
using MahjongScorekeeper.ViewModels;
using System.Linq;

namespace MahjongScorekeeper.Views;

public partial class PlayedGamesPageView : UserControl
{
    public PlayedGamesPageView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (Games.DataContext is PlayedGamesPageViewModel viewModel)
        {
            ExtendedGame? firstGame = viewModel.ExtendedGames.FirstOrDefault();

            if (firstGame != null)
            {
                foreach (string name in firstGame.ExtendedData.Keys)
                {
                    Games.Columns.Add(new DataGridTextColumn()
                    {
                        Header = name,
                        Binding = new Binding("ExtendedData[" + name + "]"),
                    });
                }
            }
        }
    }
}