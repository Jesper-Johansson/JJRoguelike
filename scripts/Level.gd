class_name RuinsLayout extends DungeonLayout

var tile_basic: TileInfo = TileInfo.new(0, Vector2i(0, 0))
var tile_cracked1: TileInfo = TileInfo.new(0, Vector2i (1, 1))
var tile_cracked2: TileInfo = TileInfo.new(0, Vector2i(2, 0))
var tile_cracked3: TileInfo = TileInfo.new(0, Vector2i(1, 0))

func define_tiles():
	floor_tile = TileInfo.new(0, Vector2i(0, 0))

# Called when the node enters the scene tree for the first time.
func _ready():
	super()
	#define_tiles()
	place_rooms(12)
	pass # Replace with function body.
			

func get_floor_tile():
	var roll: float = randf()
	if roll < 0.8:
		return tile_basic
	elif roll < 0.97:
		if randf() < 0.8:
			return tile_cracked1
		return tile_cracked2
		#return TileInfo.new(0, Vector2i)
	else:
		return tile_cracked3
