using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UXF;

public class TrailMakingTrialController : MonoBehaviour
{

    public GameObject panelRoot;
    public Session session;
    public Color white;
    string[] alphabet= { "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };
    //get all button in the grid
    public TrailMakingData tmd;
    private List<GameObject> getAllGrids()
    {

        List<GameObject> res = new List<GameObject>();
        foreach (Transform child in panelRoot.transform)
        {
            res.Add(child.gameObject);
        }
        return res;
    }
    private LinkedList<string> generateTextPath()
    {
        LinkedList<string> res = new LinkedList<string>();
        int nodes = session.settings.GetInt("number_nodes");
        bool number = session.settings.GetBool("number");
        bool letter = session.settings.GetBool("letter");
        bool rand = true; //switch between number and letter
        int grid_num = 1; //independent count for number
        int grid_letter = 0; //independent count for letter
        for (int i = 1; i <= nodes; i++)
        {
            if (number && !letter)
            {
                res.AddLast(i.ToString());
            }else if (!number && letter)
            {
                res.AddLast(alphabet[i-1]);
            }
            else
            {
                if (rand)
                {
                    res.AddLast(grid_num.ToString());
                    grid_num++; 
                    rand = false;
                }
                else
                {
                    res.AddLast(alphabet[grid_letter]);
                    grid_letter++;
                    rand = true;
                }
            }
        }
        return res;
    }

    //Use  uxf event system to trigger this function when trail begin
    public void setUpTrail()
    {
        List<GameObject> AllGrids = getAllGrids();

        LinkedList<string> textPath = generateTextPath();

        //set data to another object

        //shuffle all grids
        AllGrids.Shuffle();

        //select a N number of child
        int nodes = session.settings.GetInt("number_nodes");
        List<GameObject> NewGrids = AllGrids.GetRange(0, nodes);

        string temp = string.Join(",", textPath.ToArray());
        //set result to UXF
        session.CurrentTrial.result["correct_path"] = temp;

        tmd.addAll(textPath);
        //looop through gameobjects and set Text value
        for (int i = 0; i < nodes; i++)
        {
            NewGrids[i].GetComponentInChildren<Text>().text = textPath.First.Value;
            textPath.RemoveFirst();
            //enable button interactable
            NewGrids[i].GetComponent<Button>().interactable = true;
        }
    }
    public void resetTrail()
    {
        List<GameObject> AllGrids = getAllGrids();
        foreach (GameObject grid in AllGrids)
        {
            grid.GetComponentInChildren<Text>().text="";
            grid.GetComponent<Image>().color = white;
            grid.GetComponent<Button>().interactable = false;
        }
    }


}
