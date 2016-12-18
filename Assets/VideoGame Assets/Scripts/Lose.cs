using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lose : MonoBehaviour
{
    public Text LoseText;

    // Use this for initialization
    void Start()
    {
        LoseText.text = "Failure";
    }
}