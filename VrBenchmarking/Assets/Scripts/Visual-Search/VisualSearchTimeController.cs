using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class VisualSearchTimeController : MonoBehaviour
{

    public UXF.Session session;
    
    public void BeginCountdown()
    {
        StartCoroutine(Countdown());
    }
    public void StopCountdown()
    {
        StopAllCoroutines();
    }
    IEnumerator ObserveDisappearDuration()
    {
        float timeForObserveObjectDisappear = session.CurrentTrial.settings.GetFloat("time_between_trials");
        
        yield return new WaitForSeconds(timeForObserveObjectDisappear);
        Debug.Log("Starting next trial from 'time controller'");
        session.BeginNextTrialSafe();
    }
    IEnumerator Countdown()
    {
        float timeoutPeriod = session.CurrentTrial.settings.GetFloat("time_limit");
        Debug.Log("Starting countdown for " + timeoutPeriod + "seconds");
        yield return new WaitForSeconds(timeoutPeriod);

        Debug.Log("Ending current trial from 'time controller'");
        session.EndCurrentTrial();

        //AudioSource.PlayClipAtPoint(nosound, transform.position, 1.0f);
        Debug.Log("Pausing before next trial");
        StartCoroutine(ObserveDisappearDuration());
    }

    public void startWaitForNextTrial(){
        Debug.Log("Pausing before next trial - called externally");
        StartCoroutine(ObserveDisappearDuration());
    }
}
