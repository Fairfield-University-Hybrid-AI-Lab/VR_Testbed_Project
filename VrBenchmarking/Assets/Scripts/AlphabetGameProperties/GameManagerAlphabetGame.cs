using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAlphabetGame : MonoBehaviour
{

    public static GameManagerAlphabetGame Instance;
    public int gameSteps = 2;

    public int[] demoAlphabetIDSequenceData;


    [Header("Alphabet Properties")]
    [Space(10)]
    public string[] allAlphabetCharacters;
    bool canUserClickAlphabetButton = false;

    [Header("Tile Properties")]
    [Space(10)]
    public AlphabetTileProperties[] tiles;


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

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].Start();
        }

        canUserClickAlphabetButton = true;
    }

    public void StartGame()
    {
        SelectRandomTileAndAlphabet();
    }

    void SelectRandomTileAndAlphabet()
    {
        if (noOfStepsInCurrentGame < UIManager.Instance.noOfTrials)
        {
            int randomAlphabetID = demoAlphabetIDSequenceData[noOfStepsInCurrentGame];
                            
            noOfStepsInCurrentGame++;


            List<int> currentStepData = new List<int>();
            currentStepData.Add(randomAlphabetID);

            string currentStep = "" + randomAlphabetID;
            memory.Add(currentStep);

            ShowARandomTile(Random.Range(0, tiles.Length), allAlphabetCharacters[randomAlphabetID]);

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
        tiles[tileID].TurnOnTile(alphabetID);
    }



   


    public void CheckAlphabetNStepsAgo()
    {
        if (canUserClickAlphabetButton)
        {
            canUserClickAlphabetButton = false;

            if (memory.Count > gameSteps)
            {
                string dataOfCurrentStep = memory[noOfStepsInCurrentGame - 1];
                Debug.Log("Current alphabet position : " + dataOfCurrentStep);
                int alphabetPosCurrent;
                int.TryParse(dataOfCurrentStep, out alphabetPosCurrent);


                string dataOf2StepsBack = memory[noOfStepsInCurrentGame - 1 - gameSteps];
                Debug.Log("Alphabet position 2 steps back : " + dataOf2StepsBack);
                int alphabetPos2StepsBack;
                int.TryParse(dataOf2StepsBack, out alphabetPos2StepsBack);

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
