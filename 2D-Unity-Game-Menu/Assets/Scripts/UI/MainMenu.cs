using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        SceneFader.instance.StartFade();
    }
    public void Play()
    {
        SceneFader.instance.FadeTo("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
