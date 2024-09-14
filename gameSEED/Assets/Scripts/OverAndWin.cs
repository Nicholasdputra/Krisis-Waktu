using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OverAndWin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject overButton;
    public GameObject winButton;
    public void overBut()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    public void winBut()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
