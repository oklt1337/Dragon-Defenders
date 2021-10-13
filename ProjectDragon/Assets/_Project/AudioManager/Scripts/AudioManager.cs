using System.Collections.Generic;
using UnityEngine;

namespace _Project.AudioManager.Scripts
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

        public void PlayAudioClipOneShot(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        public void PlayAudio(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
