using System.Collections.Generic;

namespace MahjongScorekeeper.Models;

public class GameCollection
{
    public List<Game> Games { get; set; } = [];

    public void SetTo(GameCollection? other)
    {
        if (other != null)
        {
            Games = [..other.Games];
        }
    }
}
