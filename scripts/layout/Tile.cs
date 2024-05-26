using Godot;
using System.Collections.Generic;

public class Tile {
    private static readonly Dictionary<ID, TileData> _data = new Dictionary<ID, TileData>() 
    {
        {ID.RUINS_FLOOR_NORMAL, new TileData(0, new Vector2I(0, 0))},
        {ID.RUINS_FLOOR_CRACKED_1, new TileData(0, new Vector2I(1, 1))},
        {ID.RUINS_FLOOR_CRACKED_2, new TileData(0, new Vector2I(2, 0))},
        {ID.RUINS_FLOOR_CRACKED_3, new TileData(0, new Vector2I(1, 0))},
        {ID.RUINS_FLOOR_HOLE, new TileData(0, new Vector2I(3, 2))}
    };
    public static Dictionary<ID, TileData> Data
    {
        get 
        {
            return _data;
        }
    }
    public static TileData GetData(ID tileID) {
        return Data[tileID];
    }
    public enum ID {
        RUINS_FLOOR_NORMAL,
        RUINS_FLOOR_CRACKED_1,
        RUINS_FLOOR_CRACKED_2,
        RUINS_FLOOR_CRACKED_3,
        RUINS_FLOOR_HOLE
    }
}