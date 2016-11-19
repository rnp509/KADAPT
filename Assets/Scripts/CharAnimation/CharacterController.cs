using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour
{

	enum PlayerModes { Idle, Moving, Reverse };

    public float maxSpeed;
	public float maxReverseSpeed;
	public float jumpForce;
	public float incrementSpeed;
	public float lookSpeed; 

    private Rigidbody rb;
	private Animator anim;
	private bool canJump = true;
	private float currentSpeed = 0.0f;
	private Vector3 prevLoc; 
	private Vector3 currLoc;
	private PlayerModes currentMode; 		// Player's current state

    void Start()
    {
		currentMode = PlayerModes.Idle;
        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		currLoc = transform.position;
		prevLoc = currLoc;
    }

	void Update()
	{
		prevLoc = currLoc;
		currLoc = transform.position;

		switch (currentMode) {		
		case PlayerModes.Idle:
			if (Input.GetKeyDown (KeyCode.W)) {
				currentMode = PlayerModes.Moving;	
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				currentMode = PlayerModes.Reverse;
			}
			break;
		case PlayerModes.Moving:
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyUp (KeyCode.W)) {
				currentMode = PlayerModes.Idle;
			}
			break;
		case PlayerModes.Reverse:
			if (!Input.GetKey (KeyCode.S)) {
				currentMode = PlayerModes.Idle;
			}
			break;
		}

		float horizontal = Input.GetAxis ("Horizontal");

		switch (currentMode) {
		case PlayerModes.Idle:
			anim.SetBool ("isMoving", false);
			currentSpeed = Mathf.Lerp (currentSpeed, 0.0f, 5.0f * Time.deltaTime);			
			break;
		
		case PlayerModes.Moving: 
			anim.SetBool ("isMoving", true);
			currentSpeed = Mathf.Lerp (currentSpeed, maxSpeed, Time.deltaTime);
			break;

		case PlayerModes.Reverse:
			anim.SetBool ("isMoving", true);
			currentSpeed = Mathf.Lerp(currentSpeed, maxReverseSpeed, Time.deltaTime);
			break;
		}
		transform.Rotate (0.0f, horizontal, 0.0f, Space.Self); 		// Rotate player

		anim.SetFloat ("VelocityX", 5 * horizontal);
		anim.SetFloat ("VelocityZ", currentSpeed);

		transform.position += transform.forward * currentSpeed * Time.deltaTime;   					// Move forward-reverse
		//transform.position += transform.right * horizontal * Time.deltaTime;						// Move left-right

		// Jump
		if (Input.GetKeyDown (KeyCode.Space) && canJump) {											
			canJump = false;
			anim.SetBool ("Jump", true);
			rb.AddForce (new Vector3 (0.0f, jumpForce, 0.0f));
		} else if (!canJump) {
			anim.SetBool ("Jump", false);
		}
			
		
	}
	// Reset canJump
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Ground") { 
			canJump = true;
		}
	}
		
}

