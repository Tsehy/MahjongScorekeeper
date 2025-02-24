using MahjongScorekeeper.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MahjongScorekeeper.Services;

public class FileService
{
    private readonly string _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Avalonia.MahjongTracker");
    private readonly string _settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Avalonia.MahjongTracker", "settings.json");

    public async Task SaveSettingsToFileAsync(AppSettings settings)
    {
        Directory.CreateDirectory(_folderPath);

        using FileStream fs = File.Create(_settingsPath);

        await JsonSerializer.SerializeAsync(fs, settings);
    }

    public async Task<AppSettings?> ReadSettingsFromFileAsync()
    {
        try
        {
            using FileStream fs = File.OpenRead(_settingsPath);

            return await JsonSerializer.DeserializeAsync<AppSettings?>(fs);
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            return null;
        }
    }

    public async Task SaveGamesToFileAsync(GameCollection games, string fileName)
    {
        Directory.CreateDirectory(_folderPath);

        string fullFilePath = Path.Combine(_folderPath, $"{fileName}.json");

        using FileStream fs = File.Create(fullFilePath);

        await JsonSerializer.SerializeAsync(fs, games);
    }

    public async Task<GameCollection?> ReadGamesFromFileAsync(string fileName)
    {
        try
        {
            string fullFilePath = Path.Combine(_folderPath, $"{fileName}.json");

            using FileStream fs = File.OpenRead(fullFilePath);

            return await JsonSerializer.DeserializeAsync<GameCollection?>(fs);
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            return null;
        }
    }
}
