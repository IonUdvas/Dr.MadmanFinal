using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public int points = 0; 
    public int flask = 2;

    

    public GUIStyle guistyle = new GUIStyle();

    public GUIStyle guistyle2 = new GUIStyle();
    
    void OnGUI(){

        guistyle.fontSize = 50;
        guistyle.fontStyle = FontStyle.Bold;
        guistyle.normal.textColor = Color.white;

        guistyle2.fontSize = 50;
        guistyle2.fontStyle = FontStyle.Bold;
        guistyle2.normal.textColor = Color.red;   

        

        GUI.Label(new Rect(50,30,200,40), "Score: " + (points * 10),guistyle);

        if(flask > 0){
        GUI.Label(new Rect(1200,30,200,40), "You need to get " + flask + " flask(s)",guistyle2);
        }

        else{
            GUI.Label(new Rect(1200,30,200,40), "You got all flasks, \n Move towards the door \n To get to the next room.",guistyle2);
        }

        
    }
}
