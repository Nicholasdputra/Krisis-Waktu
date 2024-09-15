using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OverAndWin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject overButton;
    public GameObject winButton;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip BGMClip;
    public AudioClip clickClip;

    public void overBut()
    {
        sfxSource.PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    public void winBut()
    {
        sfxSource.PlayOneShot(clickClip);
        SceneManager.LoadScene("MainMenu");
    }
}
