using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTooSlowCheck : MonoBehaviour
{
    public GameObject observeRed;
    public AudioClip nosound;
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
        float timeForObserveObjectDisappear = session.CurrentTrial.settings.GetFloat("observe_disappear_duration");
        yield return new WaitForSeconds(timeForObserveObjectDisappear);
        session.BeginNextTrialSafe();
    }
    IEnumerator Countdown()
    {
        float timeoutPeriod = session.CurrentTrial.settings.GetFloat("timeout_period");
        yield return new WaitForSeconds(timeoutPeriod);

        session.CurrentTrial.result["outcome"] = "no";
        session.CurrentTrial.result["light"] = observeRed.activeSelf? "red":"green";
        session.EndCurrentTrial();

        AudioSource.PlayClipAtPoint(nosound, transform.position, 1.0f);

        StartCoroutine(ObserveDisappearDuration());
    }
}
