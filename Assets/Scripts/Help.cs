using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{
    public LevelLoader Level;
    private FunctionTimer functionTimer;
    // Start is called before the first frame update
    private void Start()
    {
        functionTimer = new FunctionTimer(TestingAction,20f);
    }

    private void Update(){
        functionTimer.Update();
    }

    private void TestingAction(){
        Level.LoadNextLevel(0);
    }
}
