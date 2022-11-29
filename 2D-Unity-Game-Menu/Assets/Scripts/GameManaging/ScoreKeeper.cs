using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private float score;
    public float _scoreMultiplier = 5f;
    public float ScoreMultiplier { get; set; }
    [SerializeField] Text scoreText;

    public float Score { get { return score; } }

    void Start()
    {
        ScoreMultiplier = _scoreMultiplier;
        score = 0;
    }

    void Update()
    {
        if (!GameManager.instance.IsDead)
        {
            IncrementScorePerTime();
        }
    }

    private void IncrementScorePerTime()
    {
        score += Time.deltaTime * ScoreMultiplier;
        scoreText.text = score.ToString("00");
    }
}
