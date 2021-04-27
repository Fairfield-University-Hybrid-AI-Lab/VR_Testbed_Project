using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTooSlowCheck : GonoGoLib
{
    public GameObject observeRed;
    public AudioClip nosound;

    public void BeginCountdown()
    {
        StartCoroutine(Countdown());
    }
    public void StopCountdown()
    {
        StopAllCoroutines();
    }
    IEnumerator Countdown()
    {
        float interval_duration = getIntervalBetweenEachTrial();
        yield return new WaitForSeconds(interval_duration);

        session.CurrentTrial.result["outcome"] = "no";
        session.CurrentTrial.result["light"] = observeRed.activeSelf? "red":"green";
        session.CurrentTrial.result["interval"] = interval_duration;

        session.EndCurrentTrial();

        AudioSource.PlayClipAtPoint(nosound, transform.position, 1.0f);

        StartCoroutine(ObserveDisappearDuration(interval_duration));
    }
}
