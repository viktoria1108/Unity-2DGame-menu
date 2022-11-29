using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool IsSetted { get; set; }

    private LineRenderer lineRenderer;
    private CapsuleCollider2D capsuleCollider;
    NodeMover nodeMover;
    private Vector3 startPos;
    private UnityEngine.GameObject connectedNode;
    private bool gameStarted;

    private void Start()
    {
        connectedNode = NodePool.Nodes[this];
        lineRenderer = GetComponent<LineRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        nodeMover = GetComponent<NodeMover>();

        startPos = this.transform.position;

        StartCoroutine(DisablePrimaryNodesAtStart());
    }

    private void OnEnable()
    {
        if (connectedNode != null && gameStarted)
        {
            connectedNode.SetActive(true);
            IsSetted = true;
        }
        else if(gameStarted)
        {
            this.gameObject.SetActive(false);
            connectedNode.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if(connectedNode != null)
        {
            connectedNode.SetActive(false);
            this.transform.position = startPos;
            connectedNode.transform.position = startPos;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        SetLineRenderer();
    }

    private void SetLineRenderer()
    {
        if (connectedNode != null && connectedNode.activeSelf && IsSetted)
        {
            lineRenderer.enabled = true;

            nodeMover.MoveNode(this, connectedNode);
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, connectedNode.transform.position);

            SetCollider();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void SetCollider()
    {
        Vector2 sides = new Vector2(connectedNode.transform.position.x - this.transform.position.x, connectedNode.transform.position.y - this.transform.position.y);
        float colliderLength = Mathf.Sqrt(Mathf.Pow(sides.x, 2) + Mathf.Pow(sides.y, 2)) + this.transform.localScale.x;

        Vector2 colliderSize = new Vector2(this.transform.localScale.x, colliderLength);
        Vector2 colldierOffset = new Vector2(0f, (colliderLength - this.transform.localScale.y) / 2);
        float rotationZ = Mathf.Atan2(sides.y, sides.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        capsuleCollider.offset = colldierOffset;
        capsuleCollider.size = colliderSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Node Destroyer"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DisablePrimaryNodesAtStart()
    {
        yield return new WaitForSeconds(0.1f);
        gameStarted = true;
        this.gameObject.SetActive(false);
    }
}