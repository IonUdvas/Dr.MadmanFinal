using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalPuddle : MonoBehaviour
{
    public GameObject player;

    public GameEnding gameEnding; 

    
    // public AudioSource collectAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter (Collider other){
        if(other.gameObject == player){
            gameEnding.CaughtPlayer ();
        }
    }

    
}
