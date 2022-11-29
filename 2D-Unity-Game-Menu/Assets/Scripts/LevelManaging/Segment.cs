using UnityEngine;

public class Segment : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float startPoint;
    public float endPoint;
    public int LevelLength {get; private set;}

    private Vector3 direction;

    private void Start(){
        direction = new Vector3(-LevelManager.instance.levelSpeed, 0f, 0f);
    }

    private void Update() {
        transform.Translate(direction * Time.deltaTime);
    }

    private void FixedUpdate() {
        if(transform.position.x <= endPoint)
        {
            Destroy(gameObject, 1f);
            SegmentApplier.Instance.enabled = true;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(startPoint + transform.position.x, transform.position.y, 0f), 0.15f);
        Gizmos.DrawSphere(new Vector3(endPoint + transform.position.x, transform.position.y, 0f), 0.15f);
    }
}