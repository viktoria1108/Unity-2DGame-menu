using System.Collections;
using System.Linq;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool instance;

    public Vector2 spawnIntervalRange = Vector2.zero;
    public GameObject[] powerUpPrefabs;

    private int poolSize;
    private GameObject[] powerUps;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;

        powerUpPrefabs = powerUpPrefabs.Where(item => item.GetComponent<IPowerUp>() != null).ToArray(); //Checks power ups if they derived from IPowerUp or not
        poolSize = powerUpPrefabs.Length;
        powerUps = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            powerUps[i] = Instantiate(powerUpPrefabs[i]);
            powerUps[i].SetActive(false);
        }
        
    }
    private void Start()
    {
        StartCoroutine(SpawnPickUps());
    }

    IEnumerator SpawnPickUps()
    {
        yield return new WaitForSeconds(5f);
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));
     
            GameObject tempPickUp = GetRandomPickup(poolSize);
            if(tempPickUp != null && !GameManager.instance.IsDead)
            {
                tempPickUp.transform.position = transform.position;
                tempPickUp.SetActive(true);
            }
            else break;
        }
    }

    private GameObject GetRandomPickup(int length)
    {
        int randomIndex = Random.Range(0, length);
        return powerUps[randomIndex];
    }
}
