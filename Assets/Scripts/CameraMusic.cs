using UnityEngine;

public class CameraMusic : MonoBehaviour
{
    [SerializeField] private float volume;
    [Space]
    [SerializeField] private AudioClip ambient1;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = ambient1;
        audioSource.volume = volume;
        audioSource.loop = true;
        // audioSource.Play();
    }
}
