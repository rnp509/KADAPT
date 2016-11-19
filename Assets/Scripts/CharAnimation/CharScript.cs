using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour
{

    Animator anim;
    int jumpHash = Animator.StringToHash("Jump");
    //int runStateHash = Animator.StringToHash("Base Layer.Run");

	// Use this for initialization
	void Start ()
    {

        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        anim.SetFloat("VelocityX", moveHorizontal);
        anim.SetFloat("VelocityZ", moveVertical);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpHash);
        }
    }
}
