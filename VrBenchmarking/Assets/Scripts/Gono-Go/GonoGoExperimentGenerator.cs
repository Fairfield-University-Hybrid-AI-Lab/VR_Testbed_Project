using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoExperimentGenerator : MonoBehaviour
{
    // Start is called before the first frame updat
    public void Generate(Session session)
    {
        List<int> interval = session.settings.GetIntList("ntrial_each_block");
        List<float> interval_between_each_trial = session.settings.GetFloatList("interval_duration_for_each_block");
        //making sure the number of ntrail each block and interval duration between each trail is the same
        if (interval.Count != interval_between_each_trial.Count)
        {
            Application.Quit();
        }
        foreach (int i in interval)
        {
            session.CreateBlock(i);
        }
    }
}
