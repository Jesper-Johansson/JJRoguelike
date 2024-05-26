class_name DungeonLayout extends TileMap

@export var room_pool: Array[DungeonRoomGroup]
var pool_total_weight: int = 0
var floor_tile: TileInfo: get = get_floor_tile
var current_tile: Vector2i = Vector2i(10, 10)
var rooms: Array[Room] = []

# Called when the node enters the scene tree for the first time.
func _ready():
	for group in room_pool:
		pool_total_weight += group.weight
		
		
func place_rooms(room_amount: int):
	for i in room_amount:
		var roll: int = randi_range(0, pool_total_weight)
		var weight_count: int = 0
		var chosen_group: DungeonRoomGroup
		for group in room_pool:
			if roll <= group.weight + weight_count:
				chosen_group = group
				break
			weight_count += group.weight
		
		var chosen_room: DungeonRoom = chosen_group.rooms[randi_range(0, chosen_group.rooms.size() - 1)]
		var rot: int = randi_range(1, 4)
		chosen_room = get_room_rotated(chosen_room, rot)
		
		offset_room(chosen_room, 1)
		var room_top_left: Vector2i = current_tile
		var room_bottom_right: Vector2i = Vector2i(room_top_left.x + chosen_room.row_length - 1, room_top_left.y + (chosen_room.tile_codes.length() / chosen_room.row_length) - 1)
		var new_room: Room = Room.new(room_top_left, room_bottom_right)
		new_room.room_res = chosen_room
		rooms.append(new_room)
		
	for room in rooms:
		current_tile = room.top_left
		place_room_tiles(room.room_res)
		
func place_room_tiles(room: DungeonRoom):
	var col: int = 1
	var line: int = 1
	for tile in room.tile_codes:
		if tile == "F":
			var picked_tile: TileInfo = floor_tile
			set_cell(0, current_tile, picked_tile.tile_source, picked_tile.tile_atlas_coords, 0)
		if col == room.row_length:
			col = 1
			line += 1
			current_tile.x -= room.row_length - 1
			current_tile.y += 1
		else:
			current_tile.x += 1
			col += 1

func get_room_rotated(room: DungeonRoom, rot: int) -> DungeonRoom:
	var tile_string: String = ""
	var row_count: int = room.tile_codes.length() / room.row_length
	var rotated_room: DungeonRoom = DungeonRoom.new()
	if rot == 1:
		return room
	elif rot == 2:
		var i: int = 1
		var e: int = 1
		for tile_char in room.tile_codes:
			tile_string += room.tile_codes[(4 * i) - e]
			i += 1
			if i > row_count:
				i = 1
				e += 1
		rotated_room.row_length = row_count
	elif rot == 3:
		for i in range(0, row_count):
			for e in range(room.row_length - 1, -1, -1):
				tile_string += room.tile_codes[(room.row_length * i) + e]
		rotated_room.row_length = room.row_length
	elif rot == 4:
		for i in range(0, room.row_length):
			for e in range(row_count - 1, -1, -1):
				tile_string += room.tile_codes[(e * room.row_length) + i]
		rotated_room.row_length = room.row_length
	else:
		rotated_room = room
		tile_string = room.tile_codes
	rotated_room.tile_codes = tile_string
	return rotated_room
	
func offset_room(room: DungeonRoom, extra_offset: int):
	current_tile = Vector2i(10, 10)
	var room_width: int = room.row_length
	var room_height: int = room.tile_codes.length() / room.row_length
	var extra_x_offset = randi_range(0, extra_offset)
	var extra_y_offset = extra_offset - extra_x_offset
	room_width += extra_x_offset
	room_height += extra_y_offset
	
	var offset_x_dir = 1 if randf() < 0.5 else -1
	var offset_y_dir = 1 if randf() < 0.5 else -1
	var offset_x_weight: float = randf()
	var offset_y_weight: float = randf()
	var offset_x_overlap = 0
	var offset_y_overlap = 0
	
	while(placement_overlaps(room_width, room_height, current_tile)):
		offset_x_overlap += randf() * offset_x_dir * offset_x_weight
		offset_y_overlap += randf() * offset_y_dir * offset_y_weight
		if abs(offset_x_overlap) >= 1:
			current_tile.x += int(offset_x_overlap)
			offset_x_overlap = 0
		if abs(offset_y_overlap) >= 1:
			current_tile.y += int(offset_y_overlap)
			offset_y_overlap = 0
	current_tile.x += extra_x_offset
	current_tile.y += extra_y_offset
			

func placement_overlaps(width: int, height: int, pos: Vector2i) -> bool:
	for placed_room in rooms:
		if !(pos.x > placed_room.bottom_right.x or pos.x + width < placed_room.top_left.x or pos.y + height < placed_room.top_left.y or pos.y > placed_room.bottom_right.y):
			return true
	return false

# This function does nothing, it's just here so children can override it
func get_floor_tile() -> TileInfo:
	return

class TileInfo:
	var tile_source: int
	var tile_atlas_coords: Vector2i
	
	func _init(source: int, coords: Vector2i):
		tile_source = source
		tile_atlas_coords = coords

class Room:
	var top_left: Vector2i
	var bottom_right: Vector2i
	var room_res: DungeonRoom
	
	func _init(topleft: Vector2i, bottomright: Vector2i):
		top_left = topleft
		bottom_right = bottomright
