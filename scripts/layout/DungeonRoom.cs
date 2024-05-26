using Godot;
using System;

public class DungeonRoom {
    public Vector2I Position;
    public DungeonRoomData Data;

    public DungeonRoom(Vector2I position) {
        this.Position = position;
    }
    public DungeonRoom(Vector2I position, DungeonRoomData data) : this(position) {
        this.Data = data;
    }

    public Vector2I partPosition(DungeonRoomData.RoomPart part) {
        return
            new Vector2I(Position.X + part.Bounds.TopLeft.X, Position.Y + part.Bounds.TopLeft.Y);
    }
}