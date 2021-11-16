using System.Collections.Generic;
using UnityEngine;

namespace AudioManager.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> uiSound;
        [SerializeField] private List<AudioClip> backgroundMusic;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Plays an desired audio clip.
        /// </summary>
        /// <param name="audioClip"> The desired audio clip. </param>
        public void PlayAudio(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        /// <summary>
        /// Plays an desired audio clip one shot.
        /// </summary>
        /// <param name="audioClip"> The desired audio clip. </param>
        public void PlayAudioClipOneShot(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
