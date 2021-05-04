using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;
using System;

public class VisualSearchTrialController : MonoBehaviour
{
    public GameObject displayPanel;
    public UXF.Session session;
    public VisualSearchTimeController timeController;

    public Color[] colors = new Color[] { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    public string[] colorStrings = new string[] {"green", "red", "blue", "yellow", "magenta", "cyan"};
    /*
    - OnTriggerEnter, if stick and no Trial, function BeginTrial()
    - OnTriggerEnter, if stick and current trial, record Outcome and trial duration, end trial, function BeginTrial()
    - function BeginTrial() = Observe disapper for a duration, start trial, Observe appear
    */

    // IEnumerator ObserveDisappearDuration()
    // {
    //     float timeForObserveObjectDisappear = session.CurrentTrial.settings.GetFloat("time_between_trials");
    //     yield return new WaitForSeconds(timeForObserveObjectDisappear);
    //     Debug.Log("Starting next trial");
    //     session.BeginNextTrialSafe();
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("node") && !session.InTrial && session.currentTrialNum == 0) //start trial one
        {
            session.NextTrial.Begin();
            Debug.Log("Starting first session");
        }
        else if (other.gameObject.CompareTag("node") && session.InTrial)
        {
            Debug.Log("collision event occurred");

            if(other.transform.childCount > 0){
                Text textChild = other.GetComponentInChildren<Text>();
                //Debug.Log(textChild.text);
                string displayText = displayPanel.GetComponentInChildren<Text>().text;

                //if(colorAndChar == displayText) {
                if(textChild.color.ToString() == session.CurrentTrial.result["Target_color_argb"].ToString() && textChild.text == session.CurrentTrial.result["Target_char"].ToString()) {
                    //trial successful
                    Debug.Log("Win");
                    displayPanel.GetComponentInChildren<Text>().text = "Success!";
                    session.CurrentTrial.result["outcome"] = "success";
                } else {
                    //trial fail 
                    Debug.Log("Fail");
                    displayPanel.GetComponentInChildren<Text>().text = "Fail";
                    session.CurrentTrial.result["outcome"] = "fail";
                }

                
            } 
            
            timeController.StopCountdown();
            Debug.Log("Ending current trial");
            session.EndCurrentTrial();
            //brief delay to separate object appearence from previous trial
            Debug.Log("Starting delay before next trial");
            timeController.startWaitForNextTrial();
        }
    }
}