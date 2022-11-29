using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public LivesUIController UIController;
    PlayerAnimationController animationController;
    PlayerDeathHandler deathHandler;

    [Header("Player Stats")]
    public int startLives = 3;

    [Header("Death Sequence")]

    private bool damageTaken = false;
    private float damageCountDown = 0;

    public int Lives { get; private set; }

    void Start()
    {
        Lives = startLives;

        animationController = GetComponent<PlayerAnimationController>();
        deathHandler = GetComponent<PlayerDeathHandler>();
    }

    private void Update()
    {
        if (!damageTaken) return;
        damageCountDown -= Time.deltaTime;

        if (damageCountDown > 0) return;
        damageTaken = false;
    }

    public void DecreaseLives()
    {
        if (!damageTaken)
        {
            Lives--;
            damageTaken = true;
            damageCountDown = 1;

            StartCoroutine(UIController.OnObstacleHit());
        }

        if (Lives > 0)
        {
            StartCoroutine(animationController.EnableDamageAnim());
        }
        else
        {
            StartCoroutine(deathHandler.StartGameOverSequence());
        }
    }

    
}
