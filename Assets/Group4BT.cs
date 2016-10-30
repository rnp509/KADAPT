using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

/* Story: Group of friends are meeting up but must first navigate through road obstacles */

public class Group4BT : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;	
	public Transform stop1;
	public Transform stop2;
	public GameObject targetToFollow;   // participant will follow this object if entering that object's radius
	public float followRadius; 			// size of radius to follow a leader object
	public GameObject targetToAvoid;
	public float avoidRadius;
	public GameObject participant;
	public float restartTime;

	private BehaviorAgent behaviorAgent;
	private bool hasInterest = true; 	// participant will now follow an object
	private float currTime;

	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (this.hasInterest = false) {
			currTime += Time.deltaTime;
			if (currTime >= restartTime) { 
				this.hasInterest = true;
			}
		} else {
			currTime = 0;
		}
	}

	// If approaching a road, agent stops and looks at both sides before proceeding
	protected Node ST_WatchForTraffic(Transform _stop1, Transform _stop2)
	{
		Val<Vector3> stop1Pos = Val.V (() => _stop1.position);
		Val<Vector3> stop2Pos = Val.V (() => _stop2.position);



		return null;
	}

	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

	// Follow an object if target is within the follow radius
	protected Node ST_Follow(GameObject target)
	{
		Val<Vector3> position = Val.V (() => target.transform.position);
		// Participant will follow the object for a set amount of time before losing interest
		return new Sequence( participant.GetComponent<BehaviorMecanim> ().Node_GoToUpToRadius (position, 3.0f));
	}
			
	/// Stop to speak with other agents
	protected Node ST_Converse(GameObject participant1, GameObject participant2)
	{
		return null;
	}

	protected Node BuildTreeRoot()
	{
		Val<Vector3> followPos = Val.V (() => targetToFollow.transform.position);
		Val<Vector3> avoidPos = Val.V (() => targetToAvoid.transform.position);
		Val<Vector3> participantPos = Val.V (() => participant.transform.position);
		//Func<bool> act = () => ((targetPos.Value - this.transform.position).magnitude <= followRadius); 	// Check if leader is within the designated follow radius

		// Check if participant is near the follow object
		Func<bool> nearFollow = () => (participantPos.Value - followPos.Value).magnitude <= followRadius;
		// Check if the participant is near the car
		Func<bool> nearCar = () => (participantPos.Value - avoidPos.Value).magnitude <= avoidRadius;

		Node roaming = new DecoratorLoop (
			               new SequenceShuffle (
				               this.ST_ApproachAndWait (this.wander1),
				               this.ST_ApproachAndWait (this.wander2)));
				

		Node followTrigger = new DecoratorLoop (new LeafAssert (nearFollow));
		Node follow = new DecoratorLoop (
			              new SequenceParallel (
				              this.ST_Follow (targetToFollow),
				              followTrigger));



		Node root = new DecoratorLoop (new DecoratorForceStatus (
			RunStatus.Success, 
			new Selector(follow, roaming))
		);
		return root;
	}
		
}
