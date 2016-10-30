using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public bool isSelected; 

	private Rigidbody rigidbody;
	private Color setColor;
	void Start () {
		setColor = this.GetComponent<Renderer> ().material.color;
		rigidbody = GetComponent<Rigidbody> ();
		isSelected = false;
	}
	
	void Update () {
		if (this.isSelected) { 		// Change color of agent to orange represent selection
			this.GetComponent<Renderer> ().material.color = new Color (0.9f, 0.5f, 0.3f);
		} else { 					
			this.GetComponent<Renderer> ().material.color = setColor;  // Change color back to original material color;
		}					
			

	}
}
