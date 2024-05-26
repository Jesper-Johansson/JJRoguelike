shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	COLOR = vec4(1.0, 0.0, 0.0, 0.0);
	vec2 border1 = step(vec2(0.98, 0.98), UV);
	vec2 border2 = step(vec2(0.98, 0.98), 1.0 - UV);
	COLOR.a = border1.x + border1.y + border2.x + border2.y;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
