using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class VisualSearchExperimentGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public void Generate(Session session)
    {
        int ntrial = session.settings.GetInt("ntrials");
        int timeLimit = session.settings.GetInt("time_limit");
        int colors = session.settings.GetInt("colors");
        int letters = session.settings.GetInt("letters");
        Block sess = session.CreateBlock(ntrial);
        sess.settings.SetValue("time_limit", timeLimit);
        sess.settings.SetValue("num_colors", colors);
        sess.settings.SetValue("num_letters", letters)
    }
}
