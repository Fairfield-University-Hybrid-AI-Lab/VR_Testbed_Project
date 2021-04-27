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
        //int ntrial = interval[interval.Count - 1];
        foreach (int i in interval)
        {
            session.CreateBlock(i);
        }
        //Block block1 = session.CreateBlock(ntrial);
    }
}
