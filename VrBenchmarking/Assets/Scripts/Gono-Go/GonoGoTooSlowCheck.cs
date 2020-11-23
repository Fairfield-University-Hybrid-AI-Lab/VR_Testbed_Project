using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoTooSlowCheck : MonoBehaviour
{

    public MeshRenderer observeMesh;
    public Session session;

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
        float timeoutPeriod = session.CurrentTrial.settings.GetFloat("timeout_period");
        yield return new WaitForSeconds(timeoutPeriod);

        session.CurrentTrial.result["outcome"] = "no";
        session.CurrentTrial.result["light"] = observeMesh.material.color.ToString();
        session.EndCurrentTrial();

    }
}
