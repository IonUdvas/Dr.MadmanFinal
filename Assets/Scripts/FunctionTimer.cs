using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer 
{
    private Action action;
    private float timer;

    private bool isDestroyed;


    public FunctionTimer(Action action, float timer){
        this.action = action;
        this.timer = timer;
        isDestroyed = false;
    }

    public void Update(){
        if(!isDestroyed){
            timer -= Time.deltaTime;
            if(timer < 0){
                //Trigger the action
                action();
                DestroySelf();
        }
        }
    }

    private void DestroySelf(){
        isDestroyed = true;
    }
}
