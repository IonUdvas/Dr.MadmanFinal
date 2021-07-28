using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public LevelLoader Level;
    public void PlayGame(){
        Level.LoadNextLevel(2);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ContinueGame(){
        Level.LoadNextLevel(3);
    }

    public void CreditScene(){
        Level.LoadNextLevel(5);
    }

    public void HelpScene(){
        Level.LoadNextLevel(1);
    }

    public void ReturnMenu(){
        Level.LoadNextLevel(0);
    }
}
