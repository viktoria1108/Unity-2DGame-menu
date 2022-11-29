using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] Text gameOverScoreText;

    private void OnEnable()
    {
        SetGameOverScore();
    }

    private void SetGameOverScore()
    {
        gameOverScoreText.text = $"FINAL SCORE : {Mathf.FloorToInt(scoreKeeper.Score)}";
    }

    public void RetryButton()
    {
        StartCoroutine(ButtonGoScene("MainGame"));
    }

    public void MenuButton()
    {
        StartCoroutine(ButtonGoScene("Menu"));
    }

    IEnumerator ButtonGoScene(string scene)
    {
        AudioSource.PlayClipAtPoint(GameManager.instance.buttonClickSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(GameManager.instance.buttonClickSFX.length);
        SceneFader.instance.FadeTo(scene);
    }
}
