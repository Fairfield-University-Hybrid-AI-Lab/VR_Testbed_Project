using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay Properties")]
    [Space(10)]
    public GameObject startingGameButton;
    public GameObject userInputAreaButtons;
    public GameObject settingsPanel;
    public GameObject gameCompletePanel;


    public Text noOfCorrectAnswersText;
    public Text noOfWrongAnswersText;
    public Text scoreText;

    int noOfCorrectAnswers = 0;
    int noOfWrongAnswers = 0;
    int score = 0;



    [Header("Editing Properties")]
    [Space(20)]
    public float displayDuration = 4.0f;
    public int noOfTrials = 5;

    public Text displayDurationText;
    public Text noOfTrialsText;


    [Header("Game over panel properties")]
    [Space(20)]
    public Text noOfCorrectAnswersTextGameOver;
    public Text noOfWrongAnswersTextGameOver;
    public Text scoreTextGameOver;

    public bool isPlayingAlphabetGmae = false;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startingGameButton.SetActive(true);
        userInputAreaButtons.SetActive(false);
        gameCompletePanel.SetActive(false);

        noOfCorrectAnswers = 0;
        noOfWrongAnswers = 0;
        score = 0;

        UpdateNoOfCorrectAnswersScore(0);
        UpdateNoOfWrongAnswersScore(0);
        UpdateScore(0);

        settingsPanel.SetActive(false);

        if (PlayerPrefs.GetString("DidSetupValuesOnce") != "True")
        {
            PlayerPrefs.SetString("DidSetupValuesOnce", "True");
            PlayerPrefs.SetFloat("DisplayDuration", 4.0f);
            PlayerPrefs.SetInt("NoOfTrials", 2);
        }

        displayDuration = PlayerPrefs.GetFloat("DisplayDuration");
        noOfTrials = PlayerPrefs.GetInt("NoOfTrials");

        displayDurationText.text = displayDuration + "";
        noOfTrialsText.text = noOfTrials + "";
    }


    public void ClickOnStartGameButton()
    {
        startingGameButton.SetActive(false);
        userInputAreaButtons.SetActive(true);

        if (!isPlayingAlphabetGmae)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            GameManagerAlphabetGame.Instance.StartGame();
        }
    }


    public void ClickOnPositionButton()
    {

        if (!isPlayingAlphabetGmae)
        {
            GameManager.Instance.CheckPositionNStepsAgo();
        }
        else
        {
            //GameManagerAlphabetGame.Instance.CheckPositionNStepsAgo();
        }
    }

    public void ClickOnAlphabetButton()
    {

        if (!isPlayingAlphabetGmae)
        {
            GameManager.Instance.CheckAlphabetNStepsAgo();
        }
        else
        {
            GameManagerAlphabetGame.Instance.CheckAlphabetNStepsAgo();
        }
    }

    public void ClickOnResetButton()
    {
        CancelInvoke();

        if (!isPlayingAlphabetGmae)
        {
            GameManager.Instance.StopGame();
        }
        else
        {
            GameManagerAlphabetGame.Instance.StopGame();
        }

        Start();
    }



    #region score properties
    public void UpdateNoOfCorrectAnswersScore(int addedScore)
    {
        noOfCorrectAnswers += addedScore;
        noOfCorrectAnswersText.text = "Correct ans : " + noOfCorrectAnswers;
        noOfCorrectAnswersTextGameOver.text = "Correct ans : " + noOfCorrectAnswers;
    }

    public void UpdateNoOfWrongAnswersScore(int addedScore)
    {
        noOfWrongAnswers += addedScore;
        noOfWrongAnswersText.text = "Wrong ans : " + noOfWrongAnswers;
        noOfWrongAnswersTextGameOver.text = "Wrong ans : " + noOfWrongAnswers;
    }

    public void UpdateScore(int addedScore)
    {
        score += addedScore;

        if (score < 0)
        {
            score = 0;
        }

        scoreText.text = "Score : " + score;
        scoreTextGameOver.text = "Score : " + score;
    }
    #endregion




    #region Edit settings properties
    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void ChangeValueOfDisplayDuration(float changingValue)
    {
        displayDuration += changingValue;

        if (displayDuration < 0.5f)
        {
            displayDuration = 0.5f;
        }

        if (displayDuration > 5f)
        {
            displayDuration = 5f;
        }

        displayDurationText.text = displayDuration + "";
        PlayerPrefs.SetFloat("DisplayDuration", displayDuration);
    }

    public void ChangeValueOfNoOfTrails(int changingValue)
    {
        noOfTrials += changingValue;

        if (noOfTrials < 5)
        {
            noOfTrials = 5;
        }

        if (noOfTrials > 20)
        {
            noOfTrials = 20;
        }

        noOfTrialsText.text = noOfTrials + "";
        PlayerPrefs.SetInt("NoOfTrials", noOfTrials);
    }
    #endregion
}
