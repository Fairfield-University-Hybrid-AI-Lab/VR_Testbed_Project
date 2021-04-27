using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrailMakingTooSlowToCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip nosound;
    public Session session;
    public TrailMakingData tmd;

    public void BeginCountdown()
    {
        StartCoroutine(Countdown());
    }
    public void StopCountdown()
    {
        StopAllCoroutines();
    }

    public IEnumerator ObserveDisappearDuration(float interval_duration)
    {
        yield return new WaitForSeconds(interval_duration);
        tmd.resetAllData();
        session.BeginNextTrialSafe();
    }
    IEnumerator Countdown()
    {
        float interval_duration = session.settings.GetFloat("interval_duration");
        yield return new WaitForSeconds(interval_duration);

        //result
        string temp = string.Join(",", tmd.wrong.ToArray());
        session.CurrentTrial.result["outcome"] = "fail";
        session.CurrentTrial.result["wrong_path"] = temp;

        //end trail
        session.EndCurrentTrial();
        //play fail sound
        AudioSource.PlayClipAtPoint(nosound, transform.position, 1.0f);

        //duration between each trial
        float interval_duration_between_trials = 2.0f;

        StartCoroutine(ObserveDisappearDuration(interval_duration_between_trials));
    }
}
