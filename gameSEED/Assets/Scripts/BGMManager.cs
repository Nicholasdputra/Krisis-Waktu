using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip firstBGMClip;
    public AudioClip secondBGMClip;

    private bool hasStartedSecondBGM = false;

    void Start()
    {
        // Set up the AudioSource with the first BGM clip
        bgmSource.clip = firstBGMClip;
        bgmSource.loop = false;  // The first BGM should not loop
        bgmSource.Play();
    }

    void Update()
    {
        // If the first BGM has finished and we haven't started the second BGM
        if (!hasStartedSecondBGM && !bgmSource.isPlaying)
        {
            PlaySecondBGM();
            hasStartedSecondBGM = true;
        }
    }

    void PlaySecondBGM()
    {
        bgmSource.clip = secondBGMClip;
        bgmSource.loop = true;  // The second BGM should loop indefinitely
        bgmSource.Play();
    }
}
