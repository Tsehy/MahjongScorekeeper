using System;
using MahjongScorekeeper.Data;

namespace MahjongScorekeeper.Models;

public class Game
{
    public DateOnly Date { get; set; }
    public GameType Type { get; set; }
    public GameData[] Data { get; set; } = new GameData[4];

    public Game(DateOnly date, GameType type, params GameData[] data)
    {
        Date = date;
        Type = type;
        Data = data;
    }
}
