using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioManager.Scripts
{
    public enum AudioVariables
    {
        Background,
        SfxVolume
    }
    
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private AudioSource soundSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private List<AudioClip> uiSound;
        [SerializeField] private List<AudioClip> backgroundMusic;

        public List<AudioClip> UiSound => uiSound;
        public List<AudioClip> BackgroundMusic => backgroundMusic;
        
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

        #region Public Methods

        /// <summary>
        /// Plays an desired audio clip.
        /// </summary>
        /// <param name="audioClip"> The desired audio clip. </param>
        public void PlayAudioClip(AudioClip audioClip)
        {
            soundSource.clip = audioClip;
            soundSource.Play();
        }

        /// <summary>
        /// Plays an desired audio clip one shot.
        /// </summary>
        /// <param name="audioClip"> The desired audio clip. </param>
        public void PlayAudioClipOneShot(AudioClip audioClip)
        {
            soundSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// Plays a desired music.
        /// </summary>
        /// <param name="music"></param>
        public void PlayMusic(AudioClip music)
        {
            if(musicSource.clip == music)
                return;
            
            musicSource.clip = music;
            musicSource.Play();
        }

        /// <summary>
        /// Sets the music volume to a desired value.
        /// </summary>
        /// <param name="vol"> The desired value. </param>
        public void SetMusicVolume(float vol)
        {
            masterMixer.SetFloat(AudioVariables.Background.ToString(), vol);
        }

        /// <summary>
        /// Sets the sfx volume to a desired value.
        /// </summary>
        /// <param name="vol"> The desired value. </param>
        public void SetSfxVolume(float vol)
        {
            masterMixer.SetFloat(AudioVariables.SfxVolume.ToString(), vol);
        }

        #endregion
    }
}
