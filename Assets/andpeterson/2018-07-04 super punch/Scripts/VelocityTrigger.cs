using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VelocityTrigger : MonoBehaviour {

	//CONSTS
	//amount of frames to be stored
	private const int RECORD_DEPTH = 3;

	private float cooldown = 1;

	//DATA STRUCTURES
	//positions object previously was
	private Queue<Vector3> prevPositions;

	//PUBLIC VARIABLES
	AudioSource audio;

	//VARIABLES TO BE USED
	//velocity of the object
	private float velocity = 0;
	//current position of the object
	private Vector3 currentPosition;


	// Use this for initialization
	void Start () {
		//init Queue of previous positions to have a length of RECORD_DEPTH
		prevPositions = new Queue<Vector3>(RECORD_DEPTH);

		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = this.transform.position;
		if (prevPositions.Count == RECORD_DEPTH) {
			velocity = ((currentPosition - prevPositions.Dequeue()).magnitude);
		}
		prevPositions.Enqueue (currentPosition);
		if (cooldown <= 0) {
			if (velocity >= 0.12f) {
				audio.Play ();
				Camera.main.backgroundColor = Color.white;
				//GameObject.Find("Camera (eye)").GetComponents<Camera>().
				cooldown = 1;
			}
		} else if(cooldown > 0){
			cooldown -= Time.deltaTime;
			Camera.main.backgroundColor = new Color(cooldown, cooldown, cooldown, 1);
		}
	}
}
