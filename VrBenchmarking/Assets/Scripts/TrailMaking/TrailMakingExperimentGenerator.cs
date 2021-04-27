using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrailMakingExperimentGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public void Generate(Session session)
    {
        int ntrial = session.settings.GetInt("ntrial");
        int nodes = session.settings.GetInt("number_nodes");
        if (nodes > 56)
        {
            session.End();
        }
        else
        {
            Block block = session.CreateBlock(ntrial);
        }
    }
}
