using System.Collections;
using UnityEngine;

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
    [SerializeField] private float ambientVolume;
    [SerializeField] private float combatVolume;
    [Space]
    [SerializeField] private Clip ambientByDefault = Clip.Nothing;
    [SerializeField] private Clip combatByDefault = Clip.Nothing;
    [Space]
    [SerializeField] private bool ambientLoop;
    [SerializeField] private bool combatLoop;
    [Space]
    [SerializeField] private float fromAmbientToCombatChangeSpeed;
    [SerializeField] private float fromCombatToAmbientChangeSpeed;

    [Header("Music")]
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip ambient1;
    [SerializeField] private AudioClip ambient2;
    [SerializeField] private AudioClip combat1;

    #endregion

    private void Start()
    {
        ambientAudioSource.volume = ambientVolume;
        ambientAudioSource.loop = ambientLoop;

        combatAudioSource.volume = combatVolume;
        combatAudioSource.loop = combatLoop;

        if (ambientByDefault != Clip.Nothing)
        {
            SetClip(ambientAudioSource, ambientByDefault);
            ambientAudioSource.Play();
        }
        else if (combatByDefault != Clip.Nothing)
        {
            SetClip(combatAudioSource, combatByDefault);
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
    private IEnumerator VolumeUp(AudioSource audioSource, float trackChangeSpeed, float maxVolume)
    {
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += (maxVolume / 100) * trackChangeSpeed;
            yield return new WaitForFixedUpdate();
        }
        audioSource.volume = maxVolume;
    }
    private IEnumerator VolumeDown(AudioSource audioSource, float trackChangeSpeed, float maxVolume)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= (maxVolume / 100) * trackChangeSpeed;
            yield return new WaitForFixedUpdate();
        }
        audioSource.volume = 0;
    }

    public IEnumerator SwitchFromAmbientToCombat(Clip nextClip)
    {
        SetClip(combatAudioSource, nextClip);
        combatAudioSource.Play();

        StartCoroutine(VolumeUp(combatAudioSource, fromAmbientToCombatChangeSpeed, combatVolume));
        yield return StartCoroutine(VolumeDown(ambientAudioSource, fromAmbientToCombatChangeSpeed, ambientVolume));

        ambientAudioSource.Pause();
    }
    public IEnumerator SwitchFromCombatToAmbient(Clip nextClip)
    {
        SetClip(ambientAudioSource, nextClip);
        ambientAudioSource.UnPause();

        StartCoroutine(VolumeUp(ambientAudioSource, fromCombatToAmbientChangeSpeed, ambientVolume));
        yield return StartCoroutine(VolumeDown(combatAudioSource, fromCombatToAmbientChangeSpeed, combatVolume));

        combatAudioSource.Pause();
    }

    public void TEST1()
    {
        StartCoroutine(SwitchFromAmbientToCombat(combatByDefault));
    }
    public void TEST2()
    {
        StartCoroutine(SwitchFromCombatToAmbient(ambientByDefault));
    }
}
