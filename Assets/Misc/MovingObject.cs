using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public float xOffset;			// distance to move car from

	private Vector3 point1; 		// Start point
	private Vector3 point2;		// End point

	//public Transform point1;		// Start
	//public Transform point2;		// End

	public float speed;				// Movement speed

	void Start() 
	{
		point1 = transform.position;
		point2 = transform.position;
		point2.x += xOffset;
	}

	void Update() 
	{
		transform.position = Vector3.Lerp (point1, point2, (Mathf.Sin (speed * Time.time) + 1.0f) / 2.0f);
		//transform.position = Vector3.Lerp(point1.position, point2.position, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}

}