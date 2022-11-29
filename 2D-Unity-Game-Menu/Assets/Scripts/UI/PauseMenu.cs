using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ContinueButton()
    {
        StartCoroutine(Continue());
    }

    public void RetryButton()
    {
        StartCoroutine(ButtonGoScene("MainGame"));
    }

    public void MenuButton()
    {
        StartCoroutine(ButtonGoScene("Menu"));
    }

    IEnumerator Continue()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(GameManager.instance.buttonClickSFX);
        yield return new WaitForSecondsRealtime(GameManager.instance.buttonClickSFX.length);
        GameManager.instance.pauseMenu.SetActive(false);
    }

    IEnumerator ButtonGoScene(string scene)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(GameManager.instance.buttonClickSFX);
        yield return new WaitForSecondsRealtime(GameManager.instance.buttonClickSFX.length);
        SceneFader.instance.FadeTo(scene);
    }
}
