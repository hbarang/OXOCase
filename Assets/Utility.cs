using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static List<Vector2Int> neigbourIndices = new List<Vector2Int>();

    static Utility()
    {
        neigbourIndices.Add(new Vector2Int(-1, 0));
        neigbourIndices.Add(new Vector2Int(0, 1));
        neigbourIndices.Add(new Vector2Int(1, 0));
        neigbourIndices.Add(new Vector2Int(0, -1));
    }

}

