using UnityEngine;
using System.Collections;

public class NodeSpawner : MonoBehaviour
{
    NodePool nodePool;
    private void Awake()
    {
    }
    private void Start()
    {
        nodePool = NodePool.instance;
        nodePool.enabled = false;
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        while(true)
        {
            if (GameManager.instance.IsDead) break;
            if(!nodePool.enabled)
            {
                yield return new WaitForSeconds(.5f);
                nodePool.enabled = true;
                yield return new WaitForSeconds(1f);
                nodePool.enabled = false;
            }
            else { yield return new WaitForEndOfFrame(); }
        }
    }
}