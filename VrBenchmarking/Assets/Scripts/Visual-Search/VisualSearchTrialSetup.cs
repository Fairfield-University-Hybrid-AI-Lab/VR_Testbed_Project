using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

public class VisualSearchTrialSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject displayPanel;
    //public GameObject blockPanel;

    public Color[] colors = new Color[] { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    public string[] colorStrings = new string[] {"green", "red", "blue", "yellow", "magenta", "cyan"};
    public char[] chars = new char[] { 'x', 'c', 'y', 'z', 'o', 'n' }; 

    public GameObject[] allNodes;
    public VisualSearchTimeController timeController;

    private int numColors = 1;
    //private int numObjects = 25;
    private int numChars = 2;

    //variables for target color and char indices
    private int targetColor;
    private int targetChar;

    //index of target node
    public int targetNode;

    public void init(Session session) {

	//grab settings and ensure values are within range
    
        numColors = session.settings.GetInt("colors");
        if(numColors > colors.Length) {
            numColors = colors.Length -1;
        } else if(numColors < 1) {
                numColors = 1;
            }

        numChars = session.settings.GetInt("letters");
        if(numChars > chars.Length) {
            numChars = chars.Length -1;
        } else if(numChars < 1) {
            numChars = 2;
        }

        int numTrials = session.settings.GetInt("ntrials");
        session.CreateBlock(numTrials);

        session.endAfterLastTrial = true;
    }

    void Start()
    {
        //blockPanel.setActive(true);
        allNodes = GameObject.FindGameObjectsWithTag("node");
        
    }

    public void BeginTrial(Session session)
    {
	//obscure all nodes from sight
        //blockPanel.setActive(true);

	    //pick target color and letter
        targetColor = Random.Range(0, Mathf.Min(numColors, colors.Length-1));
        targetChar = Random.Range(0, Mathf.Min(numChars, chars.Length-1));

        //Notify the player what the target is
        displayPanel.GetComponentInChildren<Text>().text = "Target: \n" + colorStrings[targetColor] + " " + chars[targetChar];

        //set up nodes
        foreach (GameObject x in allNodes) {
            Color objColor = colors[targetColor];
            char objChar = chars[targetChar];

            //ensure that the target is distinct from every other node in at least one way
            while(objColor == colors[targetColor] && objChar == chars[targetChar]) {
                objColor = randomColor();
                objChar = randomChar();
	        }

            //programmatically set the color of the text and the text itself on each node
            if(x.transform.childCount > 0) {
                Text textChild = x.GetComponentInChildren<Text>();
                textChild.text= ""+objChar;
                textChild.color = objColor;
                 
            }

        }

        //pick a target object and set its attributes - 
        int randomTarget = Random.Range(0, allNodes.Length -1);
        //target = randomTarget;
        if(allNodes[randomTarget].transform.childCount > 0) {
            Text textChild = allNodes[randomTarget].GetComponentInChildren<Text>();
            textChild.text= ""+chars[targetChar];
            textChild.color = colors[targetColor];
                 
        }
            //SET OBJECT AS A TARGET?

                   //record setup data
            session.CurrentTrial.result["Number_Colors"] = numColors;
            session.CurrentTrial.result["Number_Letters"] = numColors;
            session.CurrentTrial.result["Number_Nodes"] = allNodes.Length;
            session.CurrentTrial.result["Target_color"] = colorStrings[targetColor];
            session.CurrentTrial.result["Target_color_argb"] = allNodes[randomTarget].GetComponentInChildren<Text>().color;
            session.CurrentTrial.result["Target_char"] = chars[targetChar]; 
            session.CurrentTrial.result["Time_limit"] = session.settings.GetFloat("time_limit");
            session.CurrentTrial.result["Time_between_trials"] = session.settings.GetFloat("time_between_trials");

            timeController.BeginCountdown();
    
    }

    public void Update()
    {

    }

    //HELPER METHOD TO GET RANDOM COLOR
    Color randomColor() {
        int itemNum = Random.Range(0, numColors-1);
        return colors[itemNum];

    }

    //HELPER METHOD TO GET RANDOM CHAR
    char randomChar() {
	int itemNum = Random.Range(0, numChars-1);
        return chars[itemNum];
    }
}
