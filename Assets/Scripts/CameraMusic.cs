using UnityEngine;

public class CameraMusic : MonoBehaviour
{
    public enum Clip
    {
        Nothing,
        MainMenu,
        Ambient1,
        Ambient2
    }

    [SerializeField] private float volume;
    [SerializeField] private bool loop;
    [SerializeField] private Clip currentTrack = Clip.Nothing;
    [Space]
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip ambient1;
    [SerializeField] private AudioClip ambient2;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = loop;
    }
    private void Start()
    {
        SetClip(currentTrack);
        PlayClip();
    }

    public void SetClip(Clip clip)
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
        }
    }
    public void PlayClip()
    {
        audioSource.Play();
    }
}
