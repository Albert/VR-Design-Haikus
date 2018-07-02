using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainLogic : MonoBehaviour {

	//Enumerations
	public enum RainLevels{Low = 10, Moderate = 5, Heavy = 1};


	//Rain asset
	public Transform raindrop_prefab;

	//Rain variables
	public RainLevels rainfall;
	public int raindropLifespan;

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
		//rain speed is  0.01f is the offset
		rain_speed = (float)rainfall * 0.01f;
		//max rain = rain drops falling in a sec * lifespan of raindrops
		max_rain = (int)(1 / rain_speed * raindropLifespan);
		print ("Length: " + (int)((1 / rain_speed) * raindropLifespan));
		rain_stack = new Queue<GameObject> (max_rain) ;
		InvokeRepeating ("UpdateRain", 0f, rain_speed);
	}

	void UpdateRain () {
		position = new Vector3 (Random.Range (-5.0f, 5.0f), 5, Random.Range (-5.0f, 5.0f));
		rotation = Random.rotation;
		rain_stack.Enqueue(Instantiate(raindrop_prefab, position, rotation).gameObject);
		print (rain_stack.Count);
		if (rain_stack.Count >= max_rain - 1) {
			Destroy(rain_stack.Dequeue ());
		}
	}
}
