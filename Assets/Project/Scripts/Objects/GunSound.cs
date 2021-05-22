using System.Collections;
using System.Collections.Generic;
using Project.Classes.ScriptableObjects;
using UnityEngine;

namespace Project.Scripts.Objects
{
    public class GunSound : MonoBehaviour
    {
        [SerializeField] private GameObject audioSourcesContainer;

        private Queue<AudioSource> audioSourcesPool;
        
        private AudioClip shotSound;
        private AudioClip noAmmoSound;
        private AudioClip reloadSound;
        
        private int amountOfPooledAudioSources;

        private void Awake()
        {
            audioSourcesPool = new Queue<AudioSource>();
        }
        private void Start()
        {
            WeaponSO weaponData = GetComponent<GunController>().Weapon.WeaponData;

            shotSound = weaponData.shotSound;
            noAmmoSound = weaponData.noAmmoSound;
            reloadSound = weaponData.reloadSound;

            amountOfPooledAudioSources = (int)(weaponData.shotsPerSecond * 2);

            for (int i = 0; i < amountOfPooledAudioSources; i++)
            {
                var audioSource = CreateNewAudioSource();
                audioSourcesPool.Enqueue(audioSource);
            }
        }

        public void PlayShotSound()
        {
            StartCoroutine(PlaySound(shotSound));
        }
        public void PlayNoAmmoSound()
        {
            StartCoroutine(PlaySound(noAmmoSound));
        }
        public void PlayReloadSound()
        {
            StartCoroutine(PlaySound(reloadSound));
        }

        private IEnumerator PlaySound(AudioClip sound)
        {
            AudioSource audioSource;
            if (audioSourcesPool.Count == 0)
            {
                audioSource = CreateNewAudioSource();
            }
            else
            {
                audioSource = audioSourcesPool.Dequeue();
            }
            audioSource.enabled = true;

            audioSource.clip = sound;
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            audioSource.enabled = false;
            audioSourcesPool.Enqueue(audioSource);
        }

        private AudioSource CreateNewAudioSource()
        {
            var audioSource = audioSourcesContainer.AddComponent<AudioSource>();

            audioSource.minDistance = 1;
            audioSource.maxDistance = 50;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 1f;

            audioSource.enabled = false;
            return audioSource;
        }
    }
}
