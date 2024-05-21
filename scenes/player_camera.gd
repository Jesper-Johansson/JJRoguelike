extends Camera2D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_pressed("camera_zoom_in"):
		zoom.x += delta
		zoom.y += delta
	elif Input.is_action_pressed("camera_zoom_out"):
		zoom.x -= delta
		zoom.y -= delta

func _input(event):
	if event is InputEventMagnifyGesture:
		zoom *= event.factor
	if event is InputEventPanGesture:
		position.x += event.delta.x / zoom.x * 1.5
		position.y += event.delta.y / zoom.y * 1.5
