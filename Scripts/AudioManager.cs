using UnityEngine;

   public class AudioManager : MonoBehaviour
   {
       public AudioSource myAudioSource;

       void Start()
       {
           // Play the audio when the game starts
           myAudioSource.Play();
       }

       // You can call this method to play the audio at any point in your script
       public void PlayAudio()
       {
           myAudioSource.Play();
       }
   }