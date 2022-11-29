using UnityEngine;

public class NodeMover : MonoBehaviour
{
    [SerializeField] float pushSpeed = 10f;

    public void MoveNode(Node node, GameObject connectedNode)
    {
        if (node.transform.position.x >= -50)
        {
            node.transform.Translate(pushSpeed * Time.deltaTime * Vector2.left, Space.World);
            connectedNode.transform.Translate(pushSpeed * Time.deltaTime * Vector2.left, Space.World);
        }
        else
        {
            node.gameObject.SetActive(false);
            connectedNode.SetActive(false);
        }
    }
}
