using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileProperties : MonoBehaviour
{
    public bool isTileActivated = false;
    public GameObject colorFillObject;
    public GameObject alphabetCharText;
    
    
    public void Start()
    {
        colorFillObject.SetActive(false);
        alphabetCharText.SetActive(false);
    }

    public void TurnOnTile(string alphabetChar)
    {
        colorFillObject.SetActive(true);
        alphabetCharText.SetActive(true);

        Invoke("TurnOffTile", UIManager.Instance.displayDuration);

        alphabetCharText.GetComponent<Text>().text = alphabetChar;
    }

    void TurnOffTile()
    {
        colorFillObject.SetActive(false);
        alphabetCharText.SetActive(false);
    }
}
