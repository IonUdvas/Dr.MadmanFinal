using UnityEngine;

namespace FootstepsSystemSpace
{
    [System.Serializable]
    public class FootStepsAudioList
    {
        public string GroundTagName;
        
        [Tooltip("Add 4 audios. 2 audios for each foot. If you add more than 4, it'll reset on game start")]
        public AudioSource[] Audios = new AudioSource[4];
        
        [Tooltip("Randomize the pitch of the two audios to add variation to the sounds of the footsteps")]
        public bool randomizePitch;
        
        [Tooltip("Randomize the pitch of the two audios between these two values ~ ONLY WORKS IF THE ABOVE IS SET TO TRUE")]
        public Vector2 randomizePitchBetween = new Vector2(0.8f, 2f);
    }
}
