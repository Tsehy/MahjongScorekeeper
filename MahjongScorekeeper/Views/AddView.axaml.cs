using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.ViewModels;
using System;

namespace MahjongScorekeeper.Views;

public partial class AddView : Window
{
    public AddView()
    {
        InitializeComponent();
        
#if DEBUG
        this.AttachDevTools();
#endif
    }

    public AddView(AddViewModel dataContext) : this()
    {
        DataContext = dataContext;
    }

    public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}