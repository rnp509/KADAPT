using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour
{
    
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

}
