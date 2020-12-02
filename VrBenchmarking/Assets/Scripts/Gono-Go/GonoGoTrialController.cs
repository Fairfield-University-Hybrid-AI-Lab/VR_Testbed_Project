using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTrialController : MonoBehaviour
{
    public GameObject observeGreen;
    public UXF.Session session;
    /*
    - OnTriggerEnter, if stick and no Trial, function BeginTrial()
    - OnTriggerEnter, if stick and current trial, record Outcome and trial duration, end trial, function BeginTrial()
    - function BeginTrial() = Observe disapper for a duration, start trial, Observe appear
    */
    IEnumerator ObserveDisappearDuration()
    {
        float timeForObserveObjectDisappear = session.CurrentTrial.settings.GetFloat("observe_disappear_duration");
        yield return new WaitForSeconds(timeForObserveObjectDisappear);
        session.BeginNextTrialSafe();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick") && !session.InTrial && session.currentTrialNum == 0) //start trial one
        {
            session.NextTrial.Begin();
        }
        else if (other.gameObject.CompareTag("Stick") && session.InTrial)
        {
            
            session.CurrentTrial.result["outcome"] = "go";
            session.CurrentTrial.result["light"] = observeGreen.activeSelf ? "green":"red";
            session.EndCurrentTrial();
            //brief delay to separate object appearence from previous trial

            StartCoroutine(ObserveDisappearDuration());
        }
    }
}
