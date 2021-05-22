using System.Collections;
using UnityEngine;

namespace Project.Scripts {
    public class MusicController : MonoBehaviour
    {
        public enum Clip
        {
            Nothing,
            MainMenu,
            Ambient1,
            Ambient2,
            Combat1
        }

        #region Properties

        [Header("Components")]
        [SerializeField] private AudioSource ambientAudioSource;
        [SerializeField] private AudioSource combatAudioSource;

        [Header("Settings")]
        [SerializeField] private Clip ambient = Clip.Nothing;
        [SerializeField] private Clip combatMusic = Clip.Nothing;
        [Space]
        [SerializeField] private bool ambientLoop;
        [SerializeField] private bool combatLoop;
        [Space]
        [Range(0, 1)] [SerializeField] private float ambientVolume;
        [Range(0, 1)] [SerializeField] private float combatVolume;
        [Space]
        [SerializeField] private float ambientToCombatChangeTime;
        [SerializeField] private float combatToAmbientChangeTime;

        [Header("Music")]
        [SerializeField] private AudioClip mainMenu;
        [SerializeField] private AudioClip ambient1;
        [SerializeField] private AudioClip ambient2;
        [SerializeField] private AudioClip combat1;

        #endregion

        private void Start()
        {
            var gameState = GameState.Instance;
            gameState.OnCombatStart += StartCombatSound;
            gameState.OnCombatEnd += StartAmbientSound;

            ambientAudioSource.volume = 0;
            ambientAudioSource.loop = ambientLoop;

            combatAudioSource.volume = 0;
            combatAudioSource.loop = combatLoop;

            if (ambient != Clip.Nothing)
            {
                SetClip(ambientAudioSource, ambient);
                ambientAudioSource.volume = ambientVolume;
                ambientAudioSource.Play();
            }
            else if (combatMusic != Clip.Nothing)
            {
                SetClip(combatAudioSource, combatMusic);
                combatAudioSource.volume = combatVolume;
                combatAudioSource.Play();
            }
        }

        public void SetClip(AudioSource audioSource, Clip clip)
        {
            switch (clip)
            {
                case Clip.Nothing:
                    break;
                case Clip.MainMenu:
                    audioSource.clip = mainMenu;
                    break;
                case Clip.Ambient1:
                    audioSource.clip = ambient1;
                    break;
                case Clip.Ambient2:
                    audioSource.clip = ambient2;
                    break;
                case Clip.Combat1:
                    audioSource.clip = combat1;
                    break;
            }
        }
        private IEnumerator VolumeUp(AudioSource audioSource, float trackChangeTime, float maxVolume)
        {
            while (audioSource.volume < maxVolume)
            {
                audioSource.volume += (maxVolume / 100) * TimeToSpeed(trackChangeTime);
                yield return new WaitForFixedUpdate();
            }
            audioSource.volume = maxVolume;
        }
        private IEnumerator VolumeDown(AudioSource audioSource, float trackChangeTime, float maxVolume)
        {
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= (maxVolume / 100) * TimeToSpeed(trackChangeTime);
                yield return new WaitForFixedUpdate();
            }
            audioSource.volume = 0;
        }

        public IEnumerator SwitchFromAmbientToCombat()
        {
            SetClip(combatAudioSource, combatMusic);
            combatAudioSource.Play();

            StartCoroutine(VolumeUp(combatAudioSource, ambientToCombatChangeTime, combatVolume));
            yield return StartCoroutine(VolumeDown(ambientAudioSource, ambientToCombatChangeTime, ambientVolume));

            ambientAudioSource.Pause();
        }
        public IEnumerator SwitchFromCombatToAmbient()
        {
            SetClip(ambientAudioSource, ambient);
            ambientAudioSource.UnPause();

            StartCoroutine(VolumeUp(ambientAudioSource, combatToAmbientChangeTime, ambientVolume));
            yield return StartCoroutine(VolumeDown(combatAudioSource, combatToAmbientChangeTime, combatVolume));

            combatAudioSource.Pause();
        }

        public void StartCombatSound()
        {
            StartCoroutine(SwitchFromAmbientToCombat());
        }
        public void StartAmbientSound()
        {
            StartCoroutine(SwitchFromCombatToAmbient());
        }

        private float TimeToSpeed(float trackChangeTime)
        {
            return 2 / trackChangeTime;
        }
    }
}
