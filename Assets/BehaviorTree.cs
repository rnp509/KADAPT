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
    public GameObject Chaser1;
    public GameObject Chaser2;
    private System.Random rnd;
    private Animator anim;
    private int rn;


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

    protected Node ST_Follow(GameObject char1, GameObject char2)
    {
        Vector3 pos = new Vector3 (char2.transform.position.x, char2.transform.position.y, char2.transform.position.z + 2.0f);
        Val<Vector3> position = Val.V(() => pos);
        return new Sequence(char1.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

   // protected Node ST_Chase(GameObject char1, GameObject char2, GameObject char3)
   // {
    //    Vector3 pos = new Vector3(char2.transform.position.x, char2.transform.position.y, char2.transform.position.z + 2.0f);
     //   Val<Vector3> position = Val.V(() => pos);
    //    return new Sequence(char1.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    //}

    protected Node BuildTreeRoot()
    {
        rnd = new System.Random();
        rn = rnd.Next(1, 2);
        
        if (rn == 2)
        {
            Node roaming = new DecoratorLoop(new SequenceShuffle(this.ST_ApproachAndWait(this.point2, Explorer)));
            return roaming;
        }
        else if (rn == 1)
        {
            Node roaming = new DecoratorLoop(new SequenceShuffle(this.ST_ApproachAndWait(this.point1, Explorer)));
            Node roaming2 = new DecoratorLoop(new SequenceShuffle(this.ST_ApproachAndWait(this.point3, Sage)));
            Node follow = new DecoratorLoop(new Sequence(this.ST_Follow(Explorer, Sage)));
            Node leftPath = new DecoratorLoop(new Sequence(roaming, new SequenceParallel(roaming2,follow)));
            return leftPath;
        }
        else { return null;}
    }
}
