using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public static bool AudioState { get; private set; } = true;
    AudioSource audioSource;
    float musicVolume;

    public Button audioButton;
    [SerializeField] Sprite defaultSprite;
    public Sprite turnedOffSprite;

    private Animator audioButtonAnimator;
    private void Awake()
    {
        if (MusicPlayer.instance == null)
        {
            MusicPlayer.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicVolume = audioSource.volume;
    }

    public void ToggleMusic()
    {
        if (audioSource.volume == musicVolume)
        {
            audioSource.volume = 0;
            if (audioButton != null) audioButton.GetComponent<Image>().sprite = turnedOffSprite;

            AudioState = false;
        }
        else
        {
            audioSource.volume = musicVolume;
            if (audioButton != null) audioButton.GetComponent<Image>().sprite = defaultSprite;

            AudioState = true;
        }
    }

    IEnumerator ToggleAudioButtonAnimator()
    {
        audioButtonAnimator.enabled = true;
        yield return new WaitForSeconds(1f);
        audioButtonAnimator.enabled = false;
    }
}
