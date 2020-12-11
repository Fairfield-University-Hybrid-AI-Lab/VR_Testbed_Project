using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonoGoTrialSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject observeGreen;
    public GameObject observeRed;

    private bool Boolean;

    void Start()
    {
        observeGreen.SetActive(false);
        observeRed.SetActive(false);
    }
    void Update()
    {
        Boolean = (Random.Range(0, 2) == 0);
    }
    public void BeginTrial()
    {
        //change to object red or green
        if (Boolean)
        {
            observeGreen.SetActive(true);
        }
        else
        {
            observeRed.SetActive(true);
        }
    }
}
