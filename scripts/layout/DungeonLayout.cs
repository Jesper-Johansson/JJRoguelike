using Godot;
using System;
using System.Collections.Generic;

public abstract partial class DungeonLayout : TileMap
{
	[Export]
	private UserInterface userInterface; // This has a reference to the UI for now, move later
	protected RoomPool roomPool;
	private int totalGroupWeight = 0;
	private List<DungeonRoom> dungeonRooms = new List<DungeonRoom>();
	protected abstract RoomPool GetRoomPool();

	public readonly Vector2I MapCenter = new(0, 0);
	public override void _Ready()
	{
		roomPool = GetRoomPool();
		GD.Print(roomPool.NextRoom());
		GenerateRooms(10);

		Vector2I tileSize = TileSet.TileSize;
		foreach (DungeonRoom room in dungeonRooms) {
			foreach (DungeonRoomData.RoomPart part in room.Data.Parts) {
				Rectangle bounds = new Rectangle(
					new Vector2I((room.Position.X + part.Bounds.TopLeft.X) * tileSize.X, (room.Position.Y + part.Bounds.TopLeft.Y) * tileSize.Y),
					new Vector2I((room.Position.X + part.Bounds.TopLeft.X + part.Bounds.GetWidth()) * tileSize.X, (room.Position.Y + part.Bounds.TopLeft.Y + part.Bounds.GetHeight()) * tileSize.Y)
				);
				userInterface.AddBounds(bounds);
			}
		}
	}
	public override void _Process(double delta)
	{
		
	}
	public void GenerateRooms(int RoomAmount) 
	{
		for (int i = 0; i < RoomAmount; i++)
		{
			DungeonRoom room = new DungeonRoom(MapCenter, roomPool.NextRoom());
			offsetRoom(room);
			dungeonRooms.Add(room);
			placeRoomTiles(room);
		}
	}
	private void placeRoomTiles(DungeonRoom room) {
		foreach (DungeonRoomData.RoomPart part in room.Data.Parts) {
			Vector2I partPos = room.partPosition(part);
			for (int x = 0; x < part.Tiles.GetLength(1); x++) {  // For each column
				for (int y = 0; y < part.Tiles.GetLength(0); y++) {  // For each row
					TileData tileData = Tile.GetData(part.Tiles[y,x]);
					SetCell(0, new Vector2I(partPos.X + x, partPos.Y + y), tileData.GetSource(), tileData.GetAtlasCoords(), 0);
				}
			} 
		}
	}
	private void offsetRoom(DungeonRoom room) {
		int offsetXDir = GD.Randf() < 0.5 ? 1 : -1;
		int offsetYDir = GD.Randf() < 0.5 ? 1 : -1;
		float offsetXWeight = GD.Randf();
		float offsetYWeight = GD.Randf();
		float offsetXOverlap = 0.0f;
		float offsetYOverlap = 0.0f;

		while(roomOverlapsAnyExisting(room)) {
			offsetXOverlap += GD.Randf() * offsetXDir * offsetXWeight;
			offsetYOverlap += GD.Randf() * offsetYDir * offsetYWeight;

			if (Math.Abs(offsetXOverlap) >= 1) {
				room.Position.X += (int)offsetXOverlap;
				offsetXOverlap = 0;
			}
			if (Math.Abs(offsetYOverlap) >= 1) {
				room.Position.Y += (int)offsetYOverlap;
				offsetYOverlap = 0;
			}
		}
	}

	private bool roomOverlapsAnyExisting(DungeonRoom room) {
		foreach(DungeonRoom existing in dungeonRooms) {
			if(roomsOverlap(room, existing)) {
				return true;
			}
		}
		return false;
	}

	private bool roomsOverlap(DungeonRoom roomA, DungeonRoom roomB) {
		foreach (DungeonRoomData.RoomPart partA in roomA.Data.Parts) {
			Vector2I posA = new Vector2I(roomA.Position.X + partA.Bounds.TopLeft.X, roomA.Position.Y + partA.Bounds.TopLeft.Y);
			foreach(DungeonRoomData.RoomPart partB in roomB.Data.Parts) {
				Vector2I posB = new Vector2I(roomB.Position.X + partB.Bounds.TopLeft.X, roomB.Position.Y + partB.Bounds.TopLeft.Y);
				if ( !(posA.X > posB.X + partB.Bounds.BottomRight.X // To the right of
					|| posA.X + partA.Bounds.BottomRight.X < posB.X  // To the left of
					|| posA.Y > posB.Y + partB.Bounds.BottomRight.Y // Under
					|| posA.Y + partA.Bounds.BottomRight.Y < posB.Y // Below
					) ) {
						return true;
					}
			}
		}
		return false;
	}
}
