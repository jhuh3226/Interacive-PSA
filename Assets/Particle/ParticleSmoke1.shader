#pragma strict

@script RequireComponent(LineRenderer)

private var line : LineRenderer;
private var tr : Transform;
private var positions : Vector3[];
private var directions : Vector3[];
private var i : int;
private var timeSinceUpdate : float = 0.0;
private var lineMaterial : Material;
private var lineSegment : float = 0.0;
private var currentNumberOfPoints : int = 2;
private var allPointsAdded : boolean = false;
var numberOfPoints : int = 10;
var updateSpeed : float = 0.25;
var riseSpeed : float = 0.25;
var spread : float = 0.2;

private var tempVec : Vector3;

function Start() {
	tr = this.transform;
	line = this.GetComponent(LineRenderer);
	lineMaterial = line.material;

	lineSegment = 1.0 / numberOfPoints;

	positions = new Vector3[numberOfPoints];
	directions = new Vector3[numberOfPoints];

	line.SetVertexCount(currentNumberOfPoints);

	for (i = 0; i < currentNumberOfPoints; i++) {
		tempVec = getSmokeVec();
		directions[i] = tempVec;
		positions[i] = tr.position;
		line.SetPosition(i, positions[i]);
	}
}

function Update() {

	timeSinceUpdate += Time.deltaTime; // Update time.

									   // If it's time to update the line...
	if (timeSinceUpdate > updateSpeed) {
		timeSinceUpdate -= updateSpeed;

		// Add points until the target number is reached.
		if (!allPointsAdded) {
			currentNumberOfPoints++;
			line.SetVertexCount(currentNumberOfPoints);
			tempVec = getSmokeVec();
			directions[0] = tempVec;
			positions[0] = tr.position;
			line.SetPosition(0, positions[0]);
		}

		if (!allPointsAdded && (currentNumberOfPoints == numberOfPoints)) {
			allPointsAdded = true;
		}

		// Make each point in the line take the position and direction of the one before it (effectively removing the last point from the line and adding a new one at transform position).
		for (i = currentNumberOfPoints - 1; i > 0; i--) {
			tempVec = positions[i - 1];
			positions[i] = tempVec;
			tempVec = directions[i - 1];
			directions[i] = tempVec;
		}
		tempVec = getSmokeVec();
		directions[0] = tempVec; // Remember and give 0th point a direction for when it gets pulled up the chain in the next line update.
	}

	// Update the line...
	for (i = 1; i < currentNumberOfPoints; i++) {
		tempVec = positions[i];
		tempVec += directions[i] * Time.deltaTime;
		positions[i] = tempVec;

		line.SetPosition(i, positions[i]);
	}
	positions[0] = tr.position; // 0th point is a special case, always follows the transform directly.
	line.SetPosition(0, tr.position);

	// If we're at the maximum number of points, tweak the offset so that the last line segment is "invisible" (i.e. off the top of the texture) when it disappears.
	// Makes the change less jarring and ensures the texture doesn't jump.
	if (allPointsAdded) {
		lineMaterial.mainTextureOffset.x = lineSegment * (timeSinceUpdate / updateSpeed);
	}
}

// Give a random upwards vector.
function getSmokeVec() : Vector3{
	var smokeVec : Vector3;
smokeVec.x = Random.Range(-1.0, 1.0);
smokeVec.y = Random.Range(0.0, 1.0);
smokeVec.z = Random.Range(-1.0, 1.0);
smokeVec.Normalize();
smokeVec *= spread;
smokeVec.y += riseSpeed;
return smokeVec;
}
