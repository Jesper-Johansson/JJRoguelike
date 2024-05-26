using Godot;
using System;

public partial class RuinsRoomPool : RoomPool
{
    protected override RoomGroup[] initRoomGroups()
    {
        // This is just a placeholder way of defining layout data for now
        // I want to have a basic game prototype before I make a "level editor"
        DungeonRoomBuilder builder = new DungeonRoomBuilder();
        RoomGroup[] initGroups = {
            new RoomGroup(90, new RoomGen[]{
                // 4x4 room
                new RoomGen(() => {
                        Tile.ID[,] tiles = {
                            {basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                            {basicFloor(), GD.Randf() < 0.95f ? basicFloor() : Tile.ID.RUINS_FLOOR_HOLE, basicFloor(), basicFloor()},
                            {basicFloor(), basicFloor(), GD.Randf() < 0.95f ? basicFloor() : Tile.ID.RUINS_FLOOR_HOLE, basicFloor()},
                            {basicFloor(), basicFloor(), basicFloor(), basicFloor()}};
                        builder.AddPart(tiles, new Vector2I(0, 0));
                        return builder.BuildAndReset();
                }),
                // 6x5 room
                new RoomGen(() => {
                    Tile.ID[,] tiles = {
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()}
                    };
                    builder.AddPart(tiles, new Vector2I(0, 0));
                    return builder.BuildAndReset();
                }),
                // 5x3 room
                new RoomGen(() => {
                    Tile.ID[,] tiles = {
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()},
                        {basicFloor(), basicFloor(), basicFloor(), basicFloor(), basicFloor()}
                    };
                    builder.AddPart(tiles, new Vector2I(0, 0));
                    return builder.BuildAndReset();
                })
            })
        };
        return initGroups;
    } 
    private Tile.ID basicFloor() {
        float roll = GD.Randf();
        if (roll > 0.85f) {
            if (roll < 0.96f) {
                if (GD.Randf() < 0.5f) {
                    return Tile.ID.RUINS_FLOOR_CRACKED_1;
                }
                return Tile.ID.RUINS_FLOOR_CRACKED_2;
            }
            return Tile.ID.RUINS_FLOOR_CRACKED_3;
        }
        return Tile.ID.RUINS_FLOOR_NORMAL;
    }
}