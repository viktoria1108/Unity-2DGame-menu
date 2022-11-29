using UnityEngine;
using System.Collections;

public class LivesUIController : MonoBehaviour
{
    public GameObject LivePrefab;
    public Transform livesParent;
    public PlayerHealth PlayerHealth;

    private int livesCount;
    private GameObject[] lives;
    private int currentIndex;

    private void Start()
    {
        livesCount = PlayerHealth.startLives;
        currentIndex = livesCount - 1;
        lives = new GameObject[livesCount];

        for(int i = 0; i < livesCount; i++)
        {
            lives[i] = GameObject.Instantiate(LivePrefab);
            lives[i].transform.SetParent(livesParent);
        }
    }

    public IEnumerator OnObstacleHit()
    {
        lives[currentIndex].GetComponent<Animator>().enabled = true;
        currentIndex--;
        yield return new WaitForSeconds(5f);
        if(livesCount > 0) Destroy(lives[currentIndex + 1]);
    }
}