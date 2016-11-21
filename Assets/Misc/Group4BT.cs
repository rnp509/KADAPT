using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

/* Story: Group of friends are meeting up but must first navigate through road obstacles */

public class Group4BT : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;	
	public Transform wander3;
	public Transform wander4;

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
	protected Node ST_WatchForTraffic(Transform _stop1, Transform _stop2, GameObject car)
	{
		Val<Vector3> stop1Pos = Val.V (() => _stop1.position);
		Val<Vector3> stop2Pos = Val.V (() => _stop2.position);

		Val<Vector3> carPos = Val.V (() => car.transform.position);
		return new Sequence (participant.GetComponent<BehaviorMecanim> ().Node_HeadLook (carPos));				
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
//		Val<Vector3> participantPos = Val.V (() => participant.transform.position);

//		Func<bool> nearFollow = () => (participantPos.Value - position.Value).magnitude <= followRadius;		
		return new Sequence(participant.GetComponent<BehaviorMecanim> ().Node_GoToUpToRadius (position, 3.0f), new LeafWait(500));
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

		// Check if participant is near the follow object
		Func<bool> nearFollow = () => (participantPos.Value - followPos.Value).magnitude <= followRadius;
		Func<bool> notNearFollow = () => (participantPos.Value - followPos.Value).magnitude > followRadius;
		// Check if the participant is near the car
		Func<bool> nearCar = () => (participantPos.Value - avoidPos.Value).magnitude <= avoidRadius;
		Func<bool> notNearCar = () => (participantPos.Value - avoidPos.Value).magnitude > avoidRadius;

		/*
		// Roam between specified control points
		Node roaming = new DecoratorLoop (
			               new Sequence (
				               new LeafAssert (notNearCar), 
				               new LeafAssert (notNearFollow),
				               new SequenceShuffle (
					               new SequenceParallel (new LeafAssert (notNearCar), new LeafAssert (notNearFollow), this.ST_ApproachAndWait (this.wander1),
					               new SequenceParallel (new LeafAssert (notNearCar), new LeafAssert (notNearFollow), this.ST_ApproachAndWait (this.wander2))))));

		*/


		Node roaming = new DecoratorLoop (
			               new SelectorShuffle (
				               this.ST_ApproachAndWait (this.wander1),
				               this.ST_ApproachAndWait (this.wander2),
				               this.ST_ApproachAndWait (this.wander3), 
				               this.ST_ApproachAndWait (this.wander4)));
		
		// Follow a designated object
		Node followTrigger = new DecoratorLoop (new LeafAssert (nearFollow));
		Node follow = new DecoratorLoop (new Sequence (followTrigger, this.ST_Follow (targetToFollow)));

		// Look for incoming traffic
		Node lookAtTrigger = new DecoratorLoop (new LeafAssert(nearCar));
		Node lookAtCar = new DecoratorLoop (
			                 new Sequence (lookAtTrigger, this.ST_WatchForTraffic (this.stop1, this.stop2, targetToAvoid)));


		// Full BT
		Node root = new DecoratorLoop (new DecoratorForceStatus (
			RunStatus.Success, 
			new Selector(				
				new Sequence(lookAtTrigger, lookAtCar),
				new Sequence(followTrigger, follow),
				roaming))
		);
		return root;
	}
		
}
