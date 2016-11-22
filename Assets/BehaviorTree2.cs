using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class BehaviorTree2 : MonoBehaviour
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
        Node roaming = new DecoratorLoop(new Sequence(this.ST_ApproachAndWait(this.point3, Sage)));
        Node chase1 = new DecoratorLoop(new Sequence(this.ST_Chase1(Explorer, Chaser1)));
        Node chase2 = new DecoratorLoop(new Sequence(this.ST_Chase2(Explorer, Chaser2)));

        return roaming;
    }
}
