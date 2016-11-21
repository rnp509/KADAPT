﻿using UnityEngine;
using System.Collections;

public class FreeLookCamera : MonoBehaviour
{
	public float min_x, max_x; // Camera bound in x
	public float min_y, max_y;
	public float min_z, max_z; // Camera bound in z
	public bool canMove = true;


    float mainSpeed = 25.0f; //regular speed
    float shiftAdd = 10.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 50.0f; //Maximum speed when holdin gshift
    float camSens = 0.5f; //How sensitive it with mouse

    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    void Update()
    {
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			transform.eulerAngles = lastMouse;
		}
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;

		// Move in only x-y plane
		if (canMove) { 
			transform.Translate (p);
			newPosition.x = Mathf.Clamp (transform.position.x, min_x, max_x);
			newPosition.z = Mathf.Clamp (transform.position.z, min_z, max_z);
			newPosition.y = Mathf.Clamp (transform.position.y, min_y, max_y);
		
			transform.position = newPosition;
		}
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
		if (Input.GetKeyDown(KeyCode.Space) )
		{
			p_Velocity += new Vector3 (0, 1, 0);
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			p_Velocity += new Vector3 (0, -1, 0);
		}
        return p_Velocity;
    }
}
