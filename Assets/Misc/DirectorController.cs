using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DirectorController : MonoBehaviour {

	public Camera camera;
	public float pushForce; 

	List<GameObject> obstacles = new List<GameObject> ();

	private bool obstacleSelected = false;

	// Use this for initialization
	void Start () {
		ResetObstacles ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//ResumePathing ();

		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);

		if (Input.GetButtonDown ("Fire1")) { 
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.tag == "Obstacle") {
					ResetObstacles ();
					obstacles.Add (hit.collider.gameObject); // Add selected obstacles to obstacles list
					hit.collider.gameObject.GetComponent<ObstacleController>().isSelected = true;
					camera.gameObject.GetComponent<FreeLookCamera>().canMove = false;
					obstacleSelected = true;
				}
				
			}
		} else if (Input.GetButtonDown ("Fire2")) { // Deselect everything
			ResetObstacles (); 
		}

		if (obstacleSelected) {
			camera.gameObject.GetComponent<FreeLookCamera> ().canMove = false;			
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			obstacles[0].GetComponent<Transform> ().Translate (-pushForce * new Vector3 (horizontal, 0, vertical) * Time.deltaTime);
		} else {
			camera.gameObject.GetComponent<FreeLookCamera> ().canMove = true;
			ResetObstacles ();
		}

		
	}

	void ResetObstacles()
	{
		foreach (GameObject go in obstacles) 
		{
			go.GetComponent<ObstacleController> ().isSelected = false;
		}
		camera.gameObject.GetComponent<FreeLookCamera> ().canMove = true;
		obstacleSelected = false;
		obstacles.Clear ();
	}

}
