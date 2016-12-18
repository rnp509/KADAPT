using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
    public void startGame()
    {
        SceneManager.LoadScene("VideoGame");
    }
}
