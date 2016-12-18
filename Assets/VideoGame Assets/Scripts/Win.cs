using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Win : MonoBehaviour {

    public Text WinText;

	// Use this for initialization
	void Start ()
    {
        WinText.text = "Success!";
        Time.timeScale = 0;
    }
}
