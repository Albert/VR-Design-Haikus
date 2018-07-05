using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainLogic : MonoBehaviour {

	/* AUTHORS NOTE: I need to redo a lot of this math to make more sense*/

	//ENUMERATIONS
	//don't worry about these numebers for now
	public enum RainLevels{Low = 10, Moderate = 5, Heavy = 1};


	//PUBLIC VARIABLES
	//Rain asset
	public Transform raindrop_prefab;

	//Rain variables
	public RainLevels rainfall;
	public int raindropLifespan;

	//PRIVATE VARIABLES
	//Stores postion on rotation of new raindrop
	private Vector3 position;
	private Quaternion rotation;
	 

	//holds GameObjects for all raindrops
	private Queue<GameObject> rain_stack;

	//how often rain is created
	private float rain_speed;

	//amount of rain in existance at once
	private int max_rain;


	// Use this for initialization
	void Start () {
		//INIT RAIN VARAIBLES
		//rain speed is assigned based on the enum rainfall. 
		rain_speed = (float)rainfall * 0.01f;
		//max rain = rain drops falling in a sec * lifespan of raindrops
		max_rain = (int)(1 / rain_speed * raindropLifespan);
		//initalize the rain_stack
		rain_stack = new Queue<GameObject> (max_rain) ;
		//Invoke UpdateRain to run once everytime its time to create a raindrop based on the rain_speed
		InvokeRepeating ("UpdateRain", 0f, rain_speed);
	}

	void UpdateRain () {
		//position is random between -5 and 5 on the x and z and 5 on the y
		position = new Vector3 (Random.Range (-5.0f, 5.0f), 5, Random.Range (-5.0f, 5.0f));
		//random rotation
		rotation = Random.rotation;
		//create a new drop with the postion and rotation and add them to a queue
		rain_stack.Enqueue(Instantiate(raindrop_prefab, position, rotation).gameObject);
		//if the queue is full remove the drop that has been in it the longest (FIFO) and delete it
		if (rain_stack.Count >= max_rain - 1) {
			Destroy(rain_stack.Dequeue ());
		}
	}
}
