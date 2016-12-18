using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Text WinText;
    public Text LoseText;
    private bool GemCollected = false;
    private bool Key1Collected = false;
    private bool Key2Collected = false;
    private bool Key3Collected = false;

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

        if (other.gameObject.CompareTag("Key1"))
        {
            Key1Collected = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Key2"))
        {
            Key2Collected = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Key3"))
        {
            Key3Collected = true;
            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Desk"))
            {
                if (GemCollected)
                {
                    SceneManager.LoadScene("WinGame");
                }
            }

        if (other.gameObject.CompareTag("Guard"))
        {
            SceneManager.LoadScene("LoseGame");
        }

        if (other.gameObject.CompareTag("Vault"))
        {
            if (Key1Collected && Key2Collected && Key3Collected)
            {
                other.gameObject.SetActive(false);
            }
        }
    }


}