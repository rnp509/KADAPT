using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class BehaviorTree : MonoBehaviour
{
    private bool collidedwithc1 = false;
    private bool collidedwithc2 = false;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    public GameObject Explorer;
    public GameObject Sage;
    public GameObject Chaser1;
    public GameObject Chaser2;
    public GameObject Chest;
    private System.Random rnd;
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

    void OnTriggerEnter(Collider C)
    {
        if (C.gameObject.name == "Chaser1")
        {
            collidedwithc1 = true;
        }
        else if (C.gameObject.name == "Chaser2")
        {
            collidedwithc2 = true;
        }

    }

    protected Node ST_ApproachAndWait(Transform target, GameObject character)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_Follow(GameObject char1, GameObject char2)
    {
        Vector3 pos = new Vector3 (char2.transform.position.x + 2.0f, char2.transform.position.y, char2.transform.position.z + 1.0f);
        Val<Vector3> position = Val.V(() => pos);
        return new Sequence(char1.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_Chase1(GameObject e, GameObject c)
    {
        Vector3 pos = new Vector3(e.transform.position.x, e.transform.position.y, e.transform.position.z);
        Val<Vector3> position = Val.V(() => pos);
        if (collidedwithc1)
        {
            return new Sequence(c.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
        }
        else { return null; }
    }

    protected Node ST_Chase2(GameObject e, GameObject c)
    {
        Vector3 pos = new Vector3(e.transform.position.x, e.transform.position.y, e.transform.position.z);
        Val<Vector3> position = Val.V(() => pos);
        if (collidedwithc2)
        {
            return new Sequence(c.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
        }
        else { return null; }
    }

    protected Node BuildTreeRoot()
    {
        rnd = new System.Random();
        rn = rnd.Next(1, 3);
        
        if (rn == 2)
        {
            Node roaming = new DecoratorLoop(new Sequence(this.ST_ApproachAndWait(this.point2, Explorer)));
            Node chase1 = new DecoratorLoop(new Sequence(this.ST_Chase1(Explorer, Chaser1)));
            Node chase2 = new DecoratorLoop(new Sequence(this.ST_Chase2(Explorer, Chaser2)));
            Node rightPath = new DecoratorLoop(new Sequence(roaming, new SequenceParallel(chase1, chase2)));
            return rightPath;
        }
        else
        {
            Node roaming = this.ST_ApproachAndWait(this.point1, Explorer);
            Node roaming2 = this.ST_ApproachAndWait(this.point3, Sage);
            Node follow = this.ST_Follow(Explorer,Chest);
            Node leftPath = new Sequence(roaming, new SequenceParallel(roaming2, follow));
            return leftPath;
        }
    }
}
