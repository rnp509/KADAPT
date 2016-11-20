using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class BehaviorTree : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public GameObject Explorer;
    public GameObject Sage;


    private BehaviorAgent bAgent;
    // Use this for initialization
    void Start()
    {
        bAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(bAgent);
        bAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node ST_ApproachAndWait(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    //protected Node ST_SitDown(GameObject character)
    //{
      //  return new Sequence(character.GetComponent<BehaviorMecanim>().Node)
    //}

    protected Node BuildTreeRoot()
    {
        Node roaming = new DecoratorLoop(
                        new SequenceShuffle(
                        this.ST_ApproachAndWait(this.point1, Explorer),
                        this.ST_ApproachAndWait(this.point2, Explorer)));
        return roaming;
    }
}
