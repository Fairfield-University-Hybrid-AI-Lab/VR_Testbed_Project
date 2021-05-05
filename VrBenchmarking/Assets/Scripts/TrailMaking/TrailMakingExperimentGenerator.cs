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
        int grids = session.settings.GetInt("number_of_grids");
        if (nodes > grids)
        {
            session.End();
        }
        else
        {
            Block block = session.CreateBlock(ntrial);
        }
    }
}
