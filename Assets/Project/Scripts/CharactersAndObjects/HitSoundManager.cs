using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.CharactersAndObjects
{
    [RequireComponent(typeof(AudioSource))]
    public class HitSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            audioSource.minDistance = 1;
            audioSource.maxDistance = 50;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 1f;
        }

        public void PlayRandomClip()
        {
            var random = new Random();
            var randomIndex = random.Next(0, clips.Length - 1);
            audioSource.clip = clips[randomIndex];
            audioSource.Play();
        }
    }
}
