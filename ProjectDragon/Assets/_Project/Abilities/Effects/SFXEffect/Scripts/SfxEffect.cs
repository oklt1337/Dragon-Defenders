using System;
using UnityEngine;

namespace Abilities.Effects.SFXEffect.Scripts
{
    public class SfxEffect : MonoBehaviour
    {
        private AudioClip audioClip;
        private float coolDown;
        private float timer;

        public event Action OnPlaySound;

        public void Init(float effectCoolDown, AudioClip effectAudioClip)
        {
            audioClip = effectAudioClip;
            coolDown = effectCoolDown;
            timer = coolDown;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (!(timer <= 0)) 
                return;
            timer = coolDown;
        }

        private void OnDestroy()
        {
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClipOneShot(audioClip);
            OnPlaySound?.Invoke();
        }
    }
}