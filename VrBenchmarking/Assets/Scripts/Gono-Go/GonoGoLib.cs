using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GonoGoLib : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public UXF.Session session;
    public IEnumerator ObserveDisappearDuration(float interval_duration)
    {
        yield return new WaitForSeconds(interval_duration);

        //trigger next trial or pause for rest time
        List<int> ntrial = session.settings.GetIntList("ntrial_each_block");
        int[] interval_trial = new int[ntrial.Count];
        interval_trial[0] = ntrial[0];
        for (int i = 1; i < ntrial.Count; i++)
        {
            interval_trial[i] = interval_trial[i-1]+ntrial[i];
        }
        if (!interval_trial.Contains(session.currentTrialNum))
        {
            session.BeginNextTrialSafe();
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    public float getIntervalBetweenEachTrial()
    {
        //find timeout duration base on current trial number
        List<float> interval_between_each_trial = session.settings.GetFloatList("interval_duration_for_each_block");
        int currBlock = session.currentBlockNum;
        return interval_between_each_trial[currBlock];
    }
    //Delay after clicking button
    IEnumerator RestTimeIntervalDuration()
    {
        yield return new WaitForSeconds(2.0f);
        session.BeginNextTrialSafe();
    }
    public void RestTimeDelay()
    {
        StartCoroutine(RestTimeIntervalDuration());
    }
}
