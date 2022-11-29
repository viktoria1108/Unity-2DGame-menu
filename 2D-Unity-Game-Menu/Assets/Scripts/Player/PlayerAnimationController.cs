using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject fireballs;
    [SerializeField] private AnimationClip damageAnimClip;

    private Animator playerAnimator;
    private BoxCollider2D playerCollider;

    private bool _isFireBalls;
    private bool _isFlying;
    private int animIsFlyingID;
    private int animHasDamagedID;
    private int groundLayerMaskIndex;

    public bool IsGettingInput { get; set; }

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        groundLayerMaskIndex = LayerMask.NameToLayer("Ground");
        animIsFlyingID = Animator.StringToHash("IsFlying");
        animHasDamagedID = Animator.StringToHash("HasDamaged");

        fireballs.SetActive(false);
    }

    private void Update()
    {
        if(!ReferenceEquals(_isFireBalls, IsGettingInput))
        {
            fireballs.SetActive(IsGettingInput);
            _isFireBalls = IsGettingInput;
        }
        if(!ReferenceEquals(_isFlying, !Physics2D.IsTouchingLayers(playerCollider, groundLayerMaskIndex)))
        {
            playerAnimator.SetBool(animIsFlyingID, !Physics2D.IsTouchingLayers(playerCollider, groundLayerMaskIndex));
            _isFlying = !Physics2D.IsTouchingLayers(playerCollider, groundLayerMaskIndex);
        }
    }

    public IEnumerator EnableDamageAnim()
    {
        playerAnimator.SetBool(animHasDamagedID, true);
        yield return new WaitForSeconds(damageAnimClip.length);
        playerAnimator.SetBool(animHasDamagedID, false);
    }
}
