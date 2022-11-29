using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public ParallaxEffect[] Effects;

    private void Start()
    {
        foreach(ParallaxEffect effect in Effects)
        {
            effect.material = effect.image.material;
            effect.length = effect.image.bounds.size.x;
        }
    }

    private void FixedUpdate()
    {
        foreach(ParallaxEffect effect in Effects)
        {
            Scroll(effect);
            if(GameManager.instance.IsDead)
            {
                StartDeathSequence(effect);
            }
        }
    }

    private void Scroll(ParallaxEffect item)
    {
        item.material.mainTextureOffset += new Vector2(item.parallaxEffect * Time.fixedDeltaTime, 0f);
    }

    void StartDeathSequence(ParallaxEffect item)
    {

        item.parallaxEffect -= item.reduceAmountPerTime * 0.1f * Time.deltaTime;

        if (item.parallaxEffect <= 0)
        {
            item.parallaxEffect = 0;
            this.enabled = false;
        }
    }
}

#region Parallax Class
[System.Serializable]
public class ParallaxEffect
{
    public SpriteRenderer image;
    public float parallaxEffect;
    [Tooltip("This value will reduce the parallax effect value on death sequence")]
    public float reduceAmountPerTime = .1f;

    [HideInInspector] public float length;
    [HideInInspector] public Vector2 startPos;
    [HideInInspector] public Material material;
}
#endregion
