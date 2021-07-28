using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerRunning : MonoBehaviour
{

    public float timeLeft = 900.0f;

    public float timeLeftmins;
    public Text startText; // used for showing countdown from 3, 2, 1 


    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftmins = timeLeft/60;
        startText.text = "Time Remaining: " + (timeLeftmins).ToString("0") + " mins";
        
        if (timeLeft < 60){
            
            
            startText.text = "Time Remaining: " + (timeLeft).ToString("0") + " secs";
        }
        if (timeLeft < 10){
            
            startText.fontStyle = FontStyle.Bold;
            startText.text = "Time Remaining: " + (timeLeft).ToString("0.0") + "secs";
        }
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(0);
            
        }
    }
}