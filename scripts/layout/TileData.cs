using System.Numerics;
using Godot;

public class TileData {
    private int Source;
    public int GetSource() {
        return Source;
    }
    private Vector2I AtlasCoords;
    public Vector2I GetAtlasCoords() {
        return AtlasCoords;
    }

    public TileData(int Source, Vector2I AtlasCoords) {
        this.Source = Source;
        this.AtlasCoords = AtlasCoords;
    }
}