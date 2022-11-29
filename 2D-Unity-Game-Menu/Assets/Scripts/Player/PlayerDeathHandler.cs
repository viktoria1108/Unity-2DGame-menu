using System.Collections;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] float throwPow = 100f;
    [SerializeField][Range(0f, 90f)] float throwRot;

    Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public IEnumerator StartGameOverSequence()
    {
        DisableControllers();
        ThrowPlayer();

        GameManager.instance.IsDead = true;
      
        yield return new WaitForSeconds(1f);

        DisablePlayUI();
        GameManager.instance.SetGameOverMenu(true);
    }

    private void DisableControllers()
    {
        GetComponent<PlayerMovementController>().enabled = false;
    }

    private void DisablePlayUI()
    {
        GameManager.instance.playUI.SetActive(false);
    }

    private void ThrowPlayer()
    {
        playerRigidbody.velocity = (2 * Vector2.up + Vector2.left) * throwPow;

        Quaternion deathRot = Quaternion.Euler(0f, 0f, throwRot);
        transform.rotation = Quaternion.Slerp(transform.rotation, deathRot, 1f);
    }
}
