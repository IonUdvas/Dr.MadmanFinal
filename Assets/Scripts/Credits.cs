using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public LevelLoader Level;
    private FunctionTimer functionTimer;
    // Start is called before the first frame update
    private void Start()
    {
        functionTimer = new FunctionTimer(TestingAction,15f);
    }

    private void Update(){
        functionTimer.Update();
    }

    public void TestingAction(){
        Level.LoadNextLevel(0);
    }
}
