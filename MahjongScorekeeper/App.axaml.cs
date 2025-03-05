using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using MahjongScorekeeper.Data;
using MahjongScorekeeper.Factories;
using MahjongScorekeeper.Models;
using MahjongScorekeeper.Services;
using MahjongScorekeeper.ViewModels;
using MahjongScorekeeper.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MahjongScorekeeper;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private AppSettings? _settings;
    private GameCollection? _gameCollection;
    private FileService? _fileService;

    public override async void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var collection = new ServiceCollection();
        AddServices(collection);

        ServiceProvider services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainViewModel>(),
            };

            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            };
        }

        // We need to load data AFTER the MainWindow is set, because the async functions make a mess during initialization
        _fileService = services.GetRequiredService<FileService>();

        // Load settings
        _settings = services.GetRequiredService<AppSettings>();
        _settings.SetTo(await _fileService.ReadSettingsFromFileAsync());

        // Load games
        _gameCollection = services.GetRequiredService<GameCollection>();
        _gameCollection.SetTo(await _fileService.ReadGamesFromFileAsync(_settings.CurrentSeason));

        base.OnFrameworkInitializationCompleted();
    }

    private static void AddServices(ServiceCollection collection)
    {
        // Main
        collection.AddTransient<MainViewModel>();

        // Pages
        collection.AddTransient<PlayedGamesPageViewModel>();
        collection.AddTransient<PlayersPageViewModel>();
        collection.AddTransient<SeasonPageViewModel>();
        collection.AddTransient<StatisticsPageViewModel>();

        // Dialog
        collection.AddTransient<AddView>();
        collection.AddTransient<AddViewModel>();

        // Services
        collection.AddSingleton<FileService>();

        // Models
        collection.AddSingleton<AppSettings>();
        collection.AddSingleton<GameCollection>();

        // Page Factory
        collection.AddSingleton<Func<PageViewType, PageViewModel>>(x => type => type switch
        {
            PageViewType.PlayedGames => x.GetRequiredService<PlayedGamesPageViewModel>(),
            PageViewType.Players => x.GetRequiredService<PlayersPageViewModel>(),
            PageViewType.Season => x.GetRequiredService<SeasonPageViewModel>(),
            PageViewType.Statistics => x.GetRequiredService<StatisticsPageViewModel>(),
            _ => throw new NotImplementedException()
        });
        collection.AddSingleton<PageFactory>();

        // Dialog Factory
        collection.AddSingleton<Func<DialogViewType, Window>>(x => type => type switch
        {
            DialogViewType.AddGame => x.GetRequiredService<AddView>(),
            _ => throw new NotImplementedException()
        });
        collection.AddSingleton<DialogFactory>();
    }

    private bool _canClose = false;
    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        e.Cancel = !_canClose;

        if (e.Cancel)
        {
            await _fileService!.SaveSettingsToFileAsync(_settings!);
            await _fileService!.SaveGamesToFileAsync(_gameCollection!, _settings!.CurrentSeason);

            _canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}
