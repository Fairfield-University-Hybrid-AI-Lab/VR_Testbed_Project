using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrailMakingTracker : MonoBehaviour
{
    public PositionRotationTracker positionRotationTracker;
    // Start is called before the first frame update
    public void TrialBegin()
    {
        StartCoroutine(ManualRecord());
    }

    public void TrialEnd()
    {
        StopAllCoroutines();
    }

    private IEnumerator ManualRecord()
    {
        while (true)
        {
            if (positionRotationTracker.Recording) positionRotationTracker.RecordRow();
            //every millisecond 0.5 equals 500ms
            yield return new WaitForSeconds(0.5f);
        }
    }

}
