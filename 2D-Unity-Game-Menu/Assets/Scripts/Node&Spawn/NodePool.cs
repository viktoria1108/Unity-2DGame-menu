using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePool : MonoBehaviour
{
    [Header("Pool")]
    public UnityEngine.GameObject primaryNodePrefab;
    public UnityEngine.GameObject secondaryNodePrefab;
    [Range(10, 100)] public int poolSize = 50;

    [Header("Borders")]
    public Transform topBorder;
    public Transform bottomBorder;

    [Header("Spawn Values")]
    public Transform startPoint;
    [Tooltip("1 / value = time between node pairs")]
    public float spawnRate = 0.5f;
    public Vector2 randomTimeBetweenNodes; //it gets a random value between x(min) and y(max) and affects the horizonral distance between nodes
    [Range(1, 5)] public  float distanceLimitBetweenNodes;

    public static NodePool instance;

    private int nodeCount;
    private Node[] primaryNodes;
    private UnityEngine.GameObject[] secondaryNodes;
    private int currentIndex = 0;

    public static Dictionary<Node, UnityEngine.GameObject> Nodes { get; private set; }
    public bool IsSpawning { get; private set; }

    public bool hasStarted = false;
    private void Awake()
    {
        nodeCount = poolSize;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;

        if (!hasStarted)
        {
            primaryNodes = new Node[nodeCount];
            secondaryNodes = new UnityEngine.GameObject[nodeCount];
            Nodes = new Dictionary<Node, UnityEngine.GameObject>();
            
            hasStarted = true;
            for(int i = 0; i < nodeCount; i++) 
            {
                primaryNodes[i] = Instantiate(primaryNodePrefab, startPoint.position, Quaternion.identity).GetComponent<Node>();
                secondaryNodes[i] = Instantiate(secondaryNodePrefab, startPoint.position, Quaternion.identity);

                secondaryNodes[i].transform.SetParent(this.transform, true);
                primaryNodes[i].gameObject.transform.SetParent(this.transform, true);

                Nodes.Add(primaryNodes[i], secondaryNodes[i]); //Added them to dictionary for accessing them as pairs
            }
        }
        hasStarted = true;
        this.enabled = false;
    }

    private void OnEnable()
    {
        if(hasStarted)
        StartCoroutine(ImplementNodes(primaryNodes[currentIndex], secondaryNodes[currentIndex]));
    }

    public IEnumerator ImplementNodes(Node _primaryNode, UnityEngine.GameObject _secondaryNode)
    {
        if (GameManager.instance.IsDead) yield break;
        Vector3 randomPos = GetRandomPos();
        _primaryNode.transform.position = randomPos;

        _primaryNode.gameObject.SetActive(true);
        yield return new WaitForSeconds(Random.Range(randomTimeBetweenNodes.x, randomTimeBetweenNodes.y));


        randomPos = GetRandomPos();
        randomPos.y = Mathf.Clamp(randomPos.y, // Clamped y value for to avoid too high obstacles
                                    _primaryNode.transform.position.y - distanceLimitBetweenNodes,
                                    _primaryNode.transform.position.y + distanceLimitBetweenNodes);

        _secondaryNode.transform.position = randomPos;
        IncrementCurrenIndex();
    }

    private void IncrementCurrenIndex()
    {
        currentIndex++;
        if (currentIndex >= nodeCount)
            currentIndex = 0;
    }

    // Gets random position
    private Vector3 GetRandomPos()
    {
        Vector3 _randomPos = new Vector3(startPoint.position.x,
                                        Random.Range(bottomBorder.position.y, topBorder.position.y),
                                        startPoint.position.z);
        return _randomPos;
    }
}
