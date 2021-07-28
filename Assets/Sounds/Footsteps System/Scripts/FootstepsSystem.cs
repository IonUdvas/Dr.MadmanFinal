using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FootstepsSystemSpace;

public class FootstepsSystem : MonoBehaviour
{
    public FootStepsAudioList[] audioLists;                         //wrapper class
    public float footstepsInterval = 0.5f;                          //the interval in which to play the footstep sounds
    
    public string currentGround { get; set; }                       //current ground tag (concrete, metal, wet, etc...)
    public LayerMask groundLayer;
    
    public float downwardRayLength = 99f;
    public string defaultTag;                                       //default tag name who's audios will play when player walks over an "Untagged" object

    RaycastHit hit;                                                 //raycast output
    bool footstepsCounter;                                          //flag that the time counter should start counting
    float footstepsTime;                                            //the added delta time for the counter
    
    bool playFootstepSound;                                         //flag that it's ok to play footstep sound and not return
    int legIndex = 0;

    struct audioData{
        public AudioSource[] audios;
        public bool randomizePitch;
        public Vector2 randomizePitchBetween;
    }

    Dictionary <string, audioData> addedAudios = new Dictionary<string, audioData>();   
    float duration;
    bool onGround;


    void Awake()
    {
        footstepsCounter = false;
        playFootstepSound = false;
        footstepsTime = 0f;

        if (audioLists.Length > 0) cacheDynamicAudios();
    }

    //ground detection ray cast
    void FixedUpdate()
    {
        //make a downward ray cast to get the ground type through it's tag
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * downwardRayLength, out hit, groundLayer)){
            currentGround = hit.collider.tag;
            onGround = true;
        }else{
            onGround = false;
        }
    }

    void Update()
    {
        //for debugging
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * downwardRayLength, Color.green);

        //if time counter flagged to true start the count
        if(footstepsCounter){
            footstepsTime += 1 * Time.deltaTime;
            if (footstepsTime >= footstepsInterval) {
                footstepsTime = 0f;
                footstepsCounter = false;
                playFootstepInvoke();
            }
        }
    }

    //call this inside update when your character is moving during interval mode
    public void PlayFootSteps()
    {
        if (footstepsCounter && !playFootstepSound)
            return;

        PlayIndexAudio(legIndex);

        playFootstepSound = false;
        StartCoroutine(FlagAudioFinished());
    }
    
    //animation event
    public void FootSound()
    {
        PlayIndexAudio(legIndex);
    }

    //call this when your character stops during interval mode
    public void ResetFootSteps()
    {
        footstepsCounter = false;
        footstepsTime = 0f;
        playFootstepSound = false;
        StopCoroutine("FlagAudioFinished");
    }

    //plays each audio source of surface type
    void PlayIndexAudio(int index)
    {
        if (!onGround) return;
        if (addedAudios.ContainsKey(currentGround)){
            
            //if found empty audio source
            if (addedAudios[currentGround].audios[index] == null) {
                Debug.LogWarning("Audio source index: " + index + " at ground tag: " + currentGround + " is empty and has been skipped");

                if (legIndex == 3) legIndex = 0;
                else legIndex++;

                return;
            }

            duration = addedAudios[currentGround].audios[index].clip.length;
            if (!addedAudios[currentGround].audios[index].isPlaying){
                if(addedAudios[currentGround].randomizePitch){
                    addedAudios[currentGround].audios[index].pitch = Random.Range(addedAudios[currentGround].randomizePitchBetween.x, addedAudios[currentGround].randomizePitchBetween.y);
                }
                addedAudios[currentGround].audios[index].Play();
            }
        }else{
            if(defaultTag != null && defaultTag.Length > 0){
                
                //if found empty audio source
                if (addedAudios[defaultTag].audios[index] == null) {
                    Debug.LogWarning("Audio source index: " + index + " at ground tag: " + defaultTag + " is empty and has been skipped");

                    if (legIndex == 3) legIndex = 0;
                    else legIndex++;

                    return;
                }

                duration = addedAudios[defaultTag].audios[index].clip.length;
                if (!addedAudios[defaultTag].audios[index].isPlaying){
                    if(addedAudios[defaultTag].randomizePitch){
                        addedAudios[defaultTag].audios[index].pitch = Random.Range(addedAudios[defaultTag].randomizePitchBetween.x, addedAudios[defaultTag].randomizePitchBetween.y);
                    }
                    addedAudios[defaultTag].audios[index].Play(); 
                }
            }
        }

        if (legIndex == 3) legIndex = 0;
        else legIndex++;
    }

    //flag the play sound state
    void playFootstepInvoke()
    {
        playFootstepSound = true;
    }
    
    //builds a data structure out of all the added data
    void cacheDynamicAudios()
    {
        List<string> tagNames = new List<string>();
        addedAudios.Clear();

        foreach (var item in audioLists) { 
            if(item.GroundTagName.Length == 0 || item.Audios[0] == null || item.Audios[1] == null){
                Debug.LogWarning("Operation stopped! Empty property field: make sure you set every ground tag name and audio sources");
                return;
            }else{
                audioData tempData;
                tempData.audios = new AudioSource[4]{item.Audios[0], item.Audios[1], item.Audios[2], item.Audios[3]};
                tempData.randomizePitch = item.randomizePitch;
                tempData.randomizePitchBetween = item.randomizePitchBetween;

                addedAudios.Add(item.GroundTagName, tempData);
                tagNames.Add(item.GroundTagName);
            }
        }

        //default tag set to first index ground tag name
        if(defaultTag.Length == 0){
            defaultTag = audioLists[0].GroundTagName;
            Debug.LogWarning($"Default Tag has been set to the very first index tag name {defaultTag}");
        }else{
            if(!tagNames.Contains(defaultTag)){
                Debug.LogWarning("Operation stopped! The Default Tag set doesn't exist within the added Ground Tag Names inside Surface Tags property");
                return;
            }
        }
    }

    //coroutine for setting play sound state
    IEnumerator FlagAudioFinished()
    {
        footstepsCounter = true;
        yield return new WaitForSeconds(duration);
    }

    //trigger methods on inspector change
    void OnValidate()
    {
        if (audioLists != null) {
            cacheDynamicAudios();

            if (audioLists.Length > 0) {
                foreach (var item in audioLists) { 
                    System.Array.Resize(ref item.Audios, 4);
                }
            }
        }
    }
}
