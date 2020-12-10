using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSearchTrialSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject displayPanel;
    public GameObject blockPanel;

    public Color[] colors = new Color[] { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    public char[] chars = new char[] { 'x', 'c', 'y', 'z', 'o', 'n' }; 

    public GameObject[] allNodes = GameObject.FindGameObjectsWithTag("node");

    private int numColors = 1;
    private int numObjects = 25;
    private int numChars = 2;

    //variables for target color and char indices
    private int targetColor;
    private int targetChar;

    //index of target node
    public int targetNode;

    public void init(Session session) {

	//grab settings and ensure values are within range

	numColors = session.settings.GetInt("colors");
        if(numColors > colors.size()) {
	    numColors = colors.size() -1;
	} else if(numColors < 1) {
            numColors = 1;
        }

	numChars = session.settings.GetInt("letters");
        if(numChars > chars.size()) {
	    numChars = chars.size() -1;
	} else if(numChars < 1) {
	    numChars = 2;
        }
    }

    void Start()
    {
        blockPanel.setActive(true);
    }

    void Update()
    {
	//obscure all nodes from sight
        blockPanel.setActive(true);

	//pick target color and letter
        targetColor = Random.Range(0, colors.size-1);
        targetChar = Random.Range(0, chars.size-1);
        for(x in allNodes) {
	    Color objColor = colors[targetColor];
 	    char objChar = chars[targetChar];

	    //ensure that the target is distinct from every other node in at least one way
            while(objColor == colors[targetColor] && objChar == chars[targetChar]) {
		objColor = RandomColor();
		objChar = RandomChar();
	    }

	    //programmatically set the color of the text and the text itself on each node
	    //NOT COMPLETE
	    x.TextMesh.setColor("_color", objColor);
	    x.TextMesh.text = objChar;

            //SET EACH OBJECT AS 'NOT TARGET'

	}

	//pick a target object and set its attributes - 
	int randomTarget = Range.Random(0, allNodes.Length -1);
	target = randomTarget;
	allNodes[randomTarget].Materials.setColor(targetColor); 
	allNodes[randomTarget].TextMesh.text = targetChar;
        //SET OBJECT AS A TARGET?

	//NOT COMPLETE! set the instructions for the next task
	displayPanel.text = "Find the $(color) $(char)", colors[targetColor].toString(), chars[targetChar];
	
    }

    public void BeginTrial()
    {
	blockPanel.setActive(false);
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
