using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class GonoGoExperimentGenerator : MonoBehaviour
{
    // Start is called before the first frame updat
    public void Generate(Session session)
    {
        int ntrial = session.settings.GetInt("ntrials");
        Block sess = session.CreateBlock(ntrial);
        sess.settings.SetValue("timeout_period", 2.0);
    }
}
