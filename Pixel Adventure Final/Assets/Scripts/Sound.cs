using UnityEngine.Audio;
using UnityEngine;

// This script does not inherit form Monobehaviour meaning this script is not meant to be attached to game objects as a component
// This script is merely a storage container for our sound effects we want to use later
[System.Serializable]
public class Sound
{  
   public string name; // The reference name for this sound effect
   public AudioClip audioClip; // The audio clip assigned to this sound


   [Range(0f, 1f)]
   public float volume;


    [Range(.1f, 3f)]
   public float pitch;


   public bool loop;


   [HideInInspector] // We hide this attribute because the AudioManager will initialize this for us
   public AudioSource audioSource;
}



