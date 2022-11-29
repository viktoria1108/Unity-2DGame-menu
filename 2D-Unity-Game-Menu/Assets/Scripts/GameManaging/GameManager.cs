using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject playUI;
    public AudioClip buttonClickSFX;
    [SerializeField] Button audioButton;
    public bool IsDead { get; set; } = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        pauseMenu.SetActive(false);
        SetGameOverMenu(false);
        SceneFader.instance.StartFade();
        SetAudioButton();
    }

    private void OnEnable()
    {
        MusicPlayer.instance.audioButton = this.audioButton;
    }
    private void OnDisable()
    {
        MusicPlayer.instance.audioButton = null;
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
    }

    public void AudioButton()
    {
        MusicPlayer.instance.ToggleMusic();
    }

    public void SetGameOverMenu(bool state)
    {
        gameOverMenu.SetActive(state);
    }

    private void SetAudioButton()
    {
        if (MusicPlayer.AudioState == false)
        {
            audioButton.GetComponent<Image>().sprite = MusicPlayer.instance.turnedOffSprite;
        }
    }
}
