using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class albert_ControllerTrail : MonoBehaviour {
	public GameObject trailPrefab;
	private int numberOfTrailThings = 250;
	private List<GameObject> trailThings;
	private List<Vector3> controllerPositions;

	void Awake () {
		trailThings = new List<GameObject>();
		controllerPositions = new List<Vector3>();
		for(int i = 0; i<numberOfTrailThings; i++) {
			GameObject go = Instantiate(trailPrefab);
			trailThings.Add(go);
			controllerPositions.Add(new Vector3(0, 0, 0));
		}
	}
	
	void Update () {
		setControllerPositions();

		int pass = 0;
		List<GameObject> toRemove = new List<GameObject>();
		foreach (GameObject go in trailThings) {
			go.transform.position = controllerPositions[pass];
			if (pass > controllerPositions.Count) {
				//toRemove.Add(go);
				//print("removed");
			} else {
				int antipass = numberOfTrailThings - pass;
				Vector3 frameDisplacement = new Vector3(antipass * 0.015f, antipass * -0.0075f, antipass * -0.015f);
				go.transform.position = go.transform.position + frameDisplacement;
			}
			pass = pass + 1;
		}
		foreach(GameObject toR in toRemove) {
			trailThings.Remove(toR);
		}
		//print(pass);
	}

	void setControllerPositions() {
		SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();
		if (controller != null && controller.transform != null) {
			controllerPositions.Add(controller.transform.position);
		}

		if (controllerPositions.Count > numberOfTrailThings) {
			controllerPositions.RemoveAt(0);
		}
	}
}