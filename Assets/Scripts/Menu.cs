using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	
	public void Restart() 
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
