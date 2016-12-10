using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class VideoGameBT : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public GameObject Guard;


    private BehaviorAgent bAgent;
    // Use this for initialization
    void Start()
    {
        //bAgent = new BehaviorAgent(this.BuildTreeRoot());
        //BehaviorManager.Instance.Register(bAgent);
        //bAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
