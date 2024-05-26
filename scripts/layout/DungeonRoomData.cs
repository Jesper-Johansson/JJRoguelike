using Godot;
using System;

public class DungeonRoomData {

    /* 
    A room should eventually contain more data, like a type (treasure room, secret room, generic room, etc)
    The reason rooms are defined in "parts" is to allow for irregular room shapes beyond just rectangles.
    One part is still a rectangle because that's what's easiest to check for overlap against when placing rooms.
    */

    public RoomPart[] Parts;
    public DungeonRoomData(RoomPart[] parts) {
        this.Parts = parts;
    }

    public class RoomPart {
        public Rectangle Bounds;
        public Tile.ID[,] Tiles;

        public RoomPart(Rectangle bounds, Tile.ID[,] tiles) {
            if (Math.Abs(Math.Abs(bounds.TopLeft.X) - Math.Abs(bounds.BottomRight.X)) != tiles.GetLength(1) - 1
            || Math.Abs(Math.Abs(bounds.TopLeft.Y) - Math.Abs(bounds.BottomRight.Y)) != tiles.GetLength(0) - 1)
                throw new Exception("Room layout error!");
            
            this.Bounds = bounds;
            this.Tiles = tiles;
        }
    }
}