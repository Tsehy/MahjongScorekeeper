using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace MahjongScorekeeper.Models;

public class GameData(string name, double score)
{
    public string Name { get; set; } = name;
    public double Score { get; set; } = score;

    public override string ToString()
    {
        return $"{Name}: {Score}";
    }
}

public static class GameDataExtension
{
    public static string GetScoreFor(this GameData[] gameDatas, string name)
    {
        return gameDatas.Where(d => d.Name == name)
            .Select(d => d.Score.ToString())
            .FirstOrDefault() ?? string.Empty;
    }

    public static GameData[] ToTournamentScore(this GameData[] gameDatas, int baseScore, int[] uma)
    {
        return gameDatas.OrderByDescending(d => d.Score)
            .Select((d, i) => new GameData(d.Name, (d.Score - baseScore) / 1_000 + uma[i]))
            .ToArray();
    }
}
