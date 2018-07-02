using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainLogic : MonoBehaviour {

	public Transform raindrop_prefab;

	//Stores postion on rotation of new raindrop


	private Vector3 position;
	private Quaternion rotation;

	//holds GameObjects for all raindrops
	private Queue<Transform> rain_stack = new Queue<Transform>(200);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		position = new Vector3 (Random.Range (-5.0f, 5.0f), 5, Random.Range (-5.0f, 5.0f));
		rotation = Random.rotation;
		rain_stack.Enqueue(Instantiate(raindrop_prefab, position, rotation));
		if (rain_stack.Count >= 200) {
			Destroy(rain_stack.Dequeue ().gameObject);
		}
	}
}
