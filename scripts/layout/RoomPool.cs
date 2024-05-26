using System;
using System.Collections.Generic;
using Godot;
public abstract class RoomPool {
    protected RoomGroup[] roomGroups;
    protected int totalWeight = 0;
    public RoomPool() {
        this.roomGroups = initRoomGroups();
        foreach (RoomGroup group in roomGroups) {
            totalWeight += group.Weight;
        }
    }

    public DungeonRoomData NextRoom() {
        Random rng = new Random();
        int roll = rng.Next(0, totalWeight + 1);
        int count = 0;
        foreach(RoomGroup group in roomGroups) {
            if(roll <= count + group.Weight) {
                RoomGen room = group.Rooms[rng.Next(0, group.Rooms.Length)];
                return room.Generate();
            }
            count += group.Weight;
        }
        throw new Exception("Failed to select a room to generate");
    }
    protected abstract RoomGroup[] initRoomGroups();
    protected class RoomGen {
        public Func<DungeonRoomData> Generate;
        public RoomGen(Func<DungeonRoomData> gen) {
            this.Generate = gen;
        }
    }
    protected class RoomGroup {
        public int Weight;
        public RoomGen[] Rooms;

        public RoomGroup(int weight, RoomGen[] rooms) {
            this.Weight = weight;
            this.Rooms = rooms;
        }
    }
    protected class DungeonRoomBuilder {
        private List<Part> Parts = new List<Part>();
        public void AddPart(Tile.ID[,] tiles, Vector2I origin) {
            Parts.Add(new Part(tiles, origin));
        }
        public DungeonRoomData BuildAndReset() {
            DungeonRoomData.RoomPart[] roomParts = new DungeonRoomData.RoomPart[Parts.Count];
            for (int i = 0; i < roomParts.Length; i++) {
                roomParts[i] = new DungeonRoomData.RoomPart(new Rectangle(Parts[i].Origin, new Vector2I(Parts[i].Origin.X + Parts[i].Tiles.GetLength(1) - 1, Parts[i].Origin.Y + Parts[i].Tiles.GetLength(0) - 1)), Parts[i].Tiles);
            }
            DungeonRoomData data = new DungeonRoomData(roomParts);
            Parts.Clear();
            return data;
        }
        private class Part {
            public Tile.ID[,] Tiles;
            public Vector2I Origin;
            public Part(Tile.ID[,] tiles, Vector2I origin) {
                this.Tiles = tiles;
                this.Origin = origin;
            }
        }
    }
}