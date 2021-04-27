using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

public class TrailMakingGenerateGridObjects : MonoBehaviour
{
    public Session session;
    public Button rootObj;
    public GameObject parent;
    public void GenerateGridObject()
    {
        int grids = session.settings.GetInt("number_of_grids");
        for (int i = 0; i < grids; i++)
        {
            Instantiate(rootObj, parent.transform,true);
        }
    }
}
