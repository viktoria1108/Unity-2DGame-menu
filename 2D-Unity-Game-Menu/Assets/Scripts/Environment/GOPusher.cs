using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPusher : MonoBehaviour
{
    [SerializeField] float pushSpeed = 10f;

    private void Update()
    {
        if (transform.position.x >= -50)
        {
            transform.Translate(pushSpeed * Time.deltaTime * Vector2.left, Space.World);
        }else
        {
            this.gameObject.SetActive(false);
        }
    }
}
