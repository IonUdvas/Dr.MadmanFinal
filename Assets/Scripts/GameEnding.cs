using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public LevelLoader Level;
    public FunctionTimer functionTimer2;
    public HealthBar healthBar;
    public TakeDamage takeDamage;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup; 
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    public CanvasGroup GameWin;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    public AudioSource healthReduce;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool tookDamage = false;
    bool m_HasAudioPlayed;
    
    
    void Start(){
        functionTimer2 = new FunctionTimer(Reset,2f);
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player && (other.GetComponent<PlayerCollect>().flask == 0))
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            if(SceneManager.GetActiveScene().buildIndex == 4){
                EndLevel (GameWin, false, exitAudio);
            }
            else{
                EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
            }
        }
        else if (m_IsPlayerCaught)
        {
            // EndLevel (caughtBackgroundImageCanvasGroup, true);
            
            if(!tookDamage){
                Damage(20);
                
            }

            if(takeDamage.currentHealth == 0){
                EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
            }

            
            
        }
        if(takeDamage.currentHealth >= 20){
            functionTimer2.Update();
        }
    }

    
    void Damage(int damage){
        tookDamage = true;

        takeDamage.currentHealth -= 20;
        if(takeDamage.currentHealth == 0){
            healthBar.SetHealth(takeDamage.currentHealth);
        }
        else{
            healthReduce.Play();
            healthBar.SetHealth(takeDamage.currentHealth);
            functionTimer2 = new FunctionTimer(Reset,2f);
        }
        // if(takeDamage.currentHealth == 20){
        //     EndLevel(caughtBackgroundImageCanvasGroup, true);
        // }
        // else{
            
        //     takeDamage.currentHealth -= 20;
        //     healthBar.SetHealth(takeDamage.currentHealth);
        //     functionTimer2 = new FunctionTimer(Reset,2f);
        // }
    }

    private void Reset(){
        m_IsPlayerCaught = false;
        tookDamage = false;
        
    }
    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if(!m_HasAudioPlayed){
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                // SceneManager.LoadScene (0);
                Level.LoadNextLevel(0);
            }
            else
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Level.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}