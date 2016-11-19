using UnityEngine;
using System.Collections;

public class AgentController : MonoBehaviour {

	public Vector3 target;
	public bool isSelected;
	public int agentNumber;

	private Rigidbody rigidbody;
	private NavMeshAgent navMeshAgent;
	private Color setColor;

	void Start () {
		isSelected = false;
		target = this.transform.position;
		rigidbody = GetComponent<Rigidbody> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		setColor = GetComponent<Renderer> ().material.color;
	}

	void Update () {
		if (this.isSelected) { 		// Change color of agent to orange represent selection
			this.GetComponent<Renderer> ().material.color = new Color (0.9f, 0.5f, 0.3f);
		} else { 					
			this.GetComponent<Renderer> ().material.color = setColor;  // Change color back to original material color;
		}					

		if (target != null)
			navMeshAgent.SetDestination (target);	

		navMeshAgent.Resume ();		
	}

	void OnTriggerStay(Collider other) 
	{		
		if (other.tag == "Player") {
			AgentController otherControl = other.gameObject.GetComponent<AgentController> ();
			if (otherControl.agentNumber < this.agentNumber) {				
				if ((Mathf.Approximately (otherControl.target.x, this.target.x) && (Mathf.Approximately (otherControl.target.z, this.target.z)))) { 
					navMeshAgent.Stop ();
				}
			}
		}
	}
		
}
