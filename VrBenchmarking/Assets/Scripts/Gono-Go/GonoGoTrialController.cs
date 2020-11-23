using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTrialController : MonoBehaviour
{

    public GameObject observe;
    public Material red;
    public Material green;
    public int randomSeed;
    public Session session;

    private Material mat;

    private void Awake()
    {
        mat = observe.GetComponent<MeshRenderer>().material;
    }
    /*
    - OnTriggerEnter, if stick and no Trial, function BeginTrial()
    - OnTriggerEnter, if stick and current trial, record Outcome and trial duration, end trial, function BeginTrial()
    - function BeginTrial() = Observe disapper for a duration, start trial, Observe appear
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick") && !session.InTrial )
        {
            BeginTrial();
        }
        if (other.gameObject.CompareTag("Stick") && session.InTrial)
        {
            session.CurrentTrial.result["outcome"] = "go";
            session.CurrentTrial.result["light"] = mat.color.ToString();
            session.EndCurrentTrial();
            BeginTrial();
        }
    }
    IEnumerator Countdown()
    {
        float timeForObserveObjectDisappear = 1f;
        yield return new WaitForSeconds(timeForObserveObjectDisappear);
    }
    private void BeginTrial()
    {
        //brief delay to separate object appearence from previous trial
        StartCoroutine(Countdown());
        //change to object to a certain color
        Random.InitState(randomSeed);
        if (Random.value.Equals(1.0))
        {
            mat = green;
        }
        else
        {
            mat = red;
        }

        session.BeginNextTrial();
    }
}
