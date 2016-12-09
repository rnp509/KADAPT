using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour
{
    public Text WinText;
    private bool GemCollected = false;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
            {
                GemCollected = true;
                other.gameObject.SetActive(false);
            }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Desk"))
            {
                if (GemCollected)
                {
                    winGame();
                }
            }
    }

    void winGame()
    {
        WinText.text = "Success!";
        Time.timeScale = 0;
    }


}
