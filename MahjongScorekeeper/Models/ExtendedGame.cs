using System.Collections.Generic;
using System.Linq;

namespace MahjongScorekeeper.Models;

public class ExtendedGame(Game connectedGame, List<string> players)
{
    public Game ConnectedGame { get; set; } = connectedGame;

    public Dictionary<string, string> ExtendedData { get; set; } = players.ToDictionary(p => p, connectedGame.Data.GetScoreFor);
}
