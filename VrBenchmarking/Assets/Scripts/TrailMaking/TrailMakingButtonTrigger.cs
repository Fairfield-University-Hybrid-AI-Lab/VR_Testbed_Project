using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

public class TrailMakingButtonTrigger : MonoBehaviour
{
    public Session session;
    public Material green;
    public Material red;
    public Material normal;
    //Data gameobject
    public GameObject Grid;
    public TrailMakingData tmd;

    //change button bg color back to white
    /*
    IEnumerator RedColorDuration()
    {
        yield return new WaitForSeconds(0.5f);
        Grid.gameObject.GetComponent<Image>().material = normal;
    }
    */
    private YieldInstruction fadeInstruction = new YieldInstruction();
    public float fadeTime;

    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
        image.color = normal.color;
    }
    IEnumerator RestTimeBetweenEachTrial()
    {
        yield return new WaitForSeconds(2.0f);
        session.BeginNextTrialSafe();
    }
    public void checkSelected()
    {
        string GridName = Grid.GetComponentInChildren<Text>().text;


        //check if last value
        if (tmd.textPath.Count > 0)
        {
            //correct
            if (tmd.textPath.First.Value == GridName)
            {
                tmd.textPath.RemoveFirst();

                Grid.GetComponent<Image>().color = green.color;
                //make button unclickable
                Grid.GetComponent<Button>().interactable = false;
            }
            else //wrong selection
            {
                Grid.GetComponent<Image>().color = red.color;
                tmd.wrong.Add(GridName);
                //delay
                StartCoroutine(FadeOut(Grid.GetComponent<Image>()));
                //then rest the material
            }

        }
        if (tmd.textPath.Count == 0)
        {
            //record data, correct path is already recorded
            string temp = string.Join(",", tmd.wrong.ToArray());
            session.CurrentTrial.result["outcome"] = "success";
            session.CurrentTrial.result["wrong_path"] = temp;

            //reset data
            tmd.resetAllData();

            //end trial
            session.EndCurrentTrial();

            //begin the next trial
            StartCoroutine(RestTimeBetweenEachTrial());

        }


    }

}
