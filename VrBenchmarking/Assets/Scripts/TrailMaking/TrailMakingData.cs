using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://docs.unity3d.com/Manual/class-ScriptableObject.html
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TrailMakingData")]
public class TrailMakingData : ScriptableObject
{
    // using uxf setting system as data storage

    public LinkedList<string> textPath = new LinkedList<string>();
    public List<string> wrong = new List<string>();
    public void resetAllData()
    {
        textPath.Clear();
        wrong.Clear();
    }
    public void addAll(LinkedList<string> Path)
    {
        foreach (string p in Path)
        {
            textPath.AddLast(p);
        }
    }
}
