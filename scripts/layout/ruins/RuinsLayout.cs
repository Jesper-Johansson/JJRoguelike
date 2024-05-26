using Godot;
using System;

public partial class RuinsLayout : DungeonLayout
{
    protected override RoomPool GetRoomPool()
    {
        return new RuinsRoomPool();
    }
}