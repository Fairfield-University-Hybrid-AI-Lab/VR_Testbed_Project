using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int gameSteps = 2;

    public int[] demoTileIDSequenceData;
    public int[] demoAlphabetIDSequenceData;


    [Header("Alphabet Properties")]
    [Space(10)]
    public string[] allAlphabetCharacters;
    bool canUserClickAlphabetButton = false;

    [Header("Tile Properties")]
    [Space(10)]
    public TileProperties[] allTiles;
    bool canUserClickPositionButton = false;


    [Header("Selection Indicators Properties")]
    [Space(10)]
    public GameObject correctChoiceIndicator;
    public GameObject wrongChoiceIndicator;


    List<string> memory = new List<string>();
    public int noOfStepsInCurrentGame = 0;







    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        StopGame();
    }


    public void StopGame()
    {
        CancelInvoke();

        noOfStepsInCurrentGame = 0;
        memory.Clear();

        HideSelectionIndicators();

        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].Start();
        }

        canUserClickAlphabetButton = true;
        canUserClickPositionButton = true;
    }

    public void StartGame()
    {
        SelectRandomTileAndAlphabet();
    }

    void SelectRandomTileAndAlphabet()
    {
        if (noOfStepsInCurrentGame < UIManager.Instance.noOfTrials)
        {

            int randomTile = Random.Range(0, allTiles.Length);
            int randomAlphabetID = Random.Range(0, allAlphabetCharacters.Length);

            //int randomTile = demoTileIDSequenceData[noOfStepsInCurrentGame];
            //int randomAlphabetID = demoAlphabetIDSequenceData[noOfStepsInCurrentGame];
                            
            noOfStepsInCurrentGame++;


            List<int> currentStepData = new List<int>();
            currentStepData.Add(randomTile);
            currentStepData.Add(randomAlphabetID);

            //memoryOfPastBacks.Add(noOfStepsInCurrentGame, currentStepData);

            string currentStep = randomTile + "," + randomAlphabetID;
            memory.Add(currentStep);

            ShowARandomTile(randomTile, allAlphabetCharacters[randomAlphabetID]);

            string currentdata = "";

            for (int i = 0; i < memory.Count; i++)
            {
                currentdata += "{" + memory[i] + "}";
            }

            EnableCheckButtons();
            Debug.Log("Current data : " + currentdata);

            Invoke("SelectRandomTileAndAlphabet", UIManager.Instance.displayDuration + 1);
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        StopGame();
        UIManager.Instance.gameCompletePanel.SetActive(true);
    }


    void ShowARandomTile(int tileID, string alphabetID)
    {
        allTiles[tileID].TurnOnTile(alphabetID);
    }



    public void CheckPositionNStepsAgo()
    {
        if (canUserClickPositionButton && canUserClickAlphabetButton)
        {
            canUserClickPositionButton = false;

            if (memory.Count > gameSteps)
            {
                string[] dataOfCurrentStep = memory[noOfStepsInCurrentGame - 1].Split(',');
                Debug.Log("Current tile position : " + dataOfCurrentStep[0]);
                int tilePosCurrent;
                int.TryParse(dataOfCurrentStep[0], out tilePosCurrent);


                string[] dataOf2StepsBack = memory[noOfStepsInCurrentGame - 1 - gameSteps].Split(',');
                Debug.Log("Tile position 2 steps back : " + dataOf2StepsBack[0]);
                int tilePos2StepsBack;
                int.TryParse(dataOf2StepsBack[0], out tilePos2StepsBack);

                if (tilePosCurrent == tilePos2StepsBack)
                {
                    Debug.Log("Voila!!!!!!!!");
                    ShowCorrectSelectionIndicator();

                    UIManager.Instance.UpdateNoOfCorrectAnswersScore(1);
                    UIManager.Instance.UpdateScore(1);
                }
                else
                {
                    Debug.Log("Error!!! Wrong choice.");
                    ShowWrongSelectionIndicator();

                    UIManager.Instance.UpdateNoOfWrongAnswersScore(1);
                    UIManager.Instance.UpdateScore(-1);
                }
            }
            else
            {
                Debug.Log("Wait for atleast " + gameSteps + " steps.");
            }
        }
    }


    public void CheckAlphabetNStepsAgo()
    {
        if (canUserClickPositionButton && canUserClickAlphabetButton)
        {
            canUserClickAlphabetButton = false;

            if (memory.Count > gameSteps)
            {
                string[] dataOfCurrentStep = memory[noOfStepsInCurrentGame - 1].Split(',');
                Debug.Log("Current alphabet position : " + dataOfCurrentStep[1]);
                int alphabetPosCurrent;
                int.TryParse(dataOfCurrentStep[1], out alphabetPosCurrent);


                string[] dataOf2StepsBack = memory[noOfStepsInCurrentGame - 1 - gameSteps].Split(',');
                Debug.Log("Alphabet position 2 steps back : " + dataOf2StepsBack[1]);
                int alphabetPos2StepsBack;
                int.TryParse(dataOf2StepsBack[1], out alphabetPos2StepsBack);

                if (alphabetPosCurrent == alphabetPos2StepsBack)
                {
                    Debug.Log("Voila!!!!!!!!");
                    ShowCorrectSelectionIndicator();

                    UIManager.Instance.UpdateNoOfCorrectAnswersScore(1);
                    UIManager.Instance.UpdateScore(1);
                }
                else
                {
                    Debug.Log("Error!!! Wrong choice.");
                    ShowWrongSelectionIndicator();

                    UIManager.Instance.UpdateNoOfWrongAnswersScore(1);
                    UIManager.Instance.UpdateScore(-1);
                }
            }
            else
            {
                Debug.Log("Wait for atleast " + gameSteps + " steps.");
            }
        }
    }




    #region choice indicator properties
    public void EnableCheckButtons()
    {
        canUserClickPositionButton = true;
        canUserClickAlphabetButton = true;
    }

    void ShowCorrectSelectionIndicator()
    {
        correctChoiceIndicator.SetActive(true);
        Invoke("HideSelectionIndicators", 1.0f);
    }

    void ShowWrongSelectionIndicator()
    {
        wrongChoiceIndicator.SetActive(true);
        Invoke("HideSelectionIndicators", 1.0f);
    }

    void HideSelectionIndicators()
    {
        correctChoiceIndicator.SetActive(false);
        wrongChoiceIndicator.SetActive(false);
    }
    #endregion
}
