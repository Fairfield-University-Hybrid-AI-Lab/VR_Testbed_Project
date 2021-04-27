using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTrialController : GonoGoLib
{
    public GameObject observeGreen;
    /*
    - OnTriggerEnter, if stick and no Trial, function BeginTrial()
    - OnTriggerEnter, if stick and current trial, record Outcome and trial duration, end trial, function BeginTrial()
    - function BeginTrial() = Observe disapper for a duration, start trial, Observe appear
    */
    IEnumerator InitialObserveDisappearDuration(float interval_duration)
    {
        yield return new WaitForSeconds(interval_duration);
        session.NextTrial.Begin();
    }
    private void OnTriggerEnter(Collider other)
    {
        float interval_duration = getIntervalBetweenEachTrial();
        if (other.gameObject.CompareTag("Stick") && !session.InTrial && session.currentTrialNum == 0) //start trial one
        {
            StartCoroutine(InitialObserveDisappearDuration(interval_duration));
        }
        else if (other.gameObject.CompareTag("Stick") && session.InTrial)
        {
            
            session.CurrentTrial.result["outcome"] = "go";
            session.CurrentTrial.result["light"] = observeGreen.activeSelf ? "green":"red";
            session.CurrentTrial.result["interval"] = interval_duration;

            session.EndCurrentTrial();
            //brief delay to separate object appearence from previous trial

            StartCoroutine(ObserveDisappearDuration(interval_duration));
        }
    }
}
