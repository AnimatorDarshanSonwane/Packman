using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites = new Sprite[0];
    public Sprite[] Sprites { get { return _sprites; } }

    public float animationTime = 0.25f;
    public bool loop = true;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    // private void Start()
    // {
    //     _animationFrame = 0;
    //     InvokeRepeating(nameof(Advance), 0, animationTime);
    // }
private IEnumerator AdvanceCoroutine()
{
    while (_spriteRenderer.enabled)
    {
        yield return new WaitForSeconds(animationTime); // Wait for animationTime before changing sprite
        if (_sprites.Length > 0)
        {
            _animationFrame++;

            if (_animationFrame >= _sprites.Length && loop)
            {
                _animationFrame = 0;
            }

            _animationFrame %= _sprites.Length; // Ensure animation frame stays within bounds

            _spriteRenderer.sprite = _sprites[_animationFrame];
            
            
            //Debug.Log("Sprite changed to: " + _spriteRenderer.sprite.name); // Log the name of the sprite for debugging
        }
        else
        {
           // Debug.LogWarning("Sprites array is empty. Ensure sprites are assigned in the Unity Editor.");
        }
    }
}

private void Start()
{
    _animationFrame = 0;
    StartCoroutine(AdvanceCoroutine());
}

    public void Restart()
    {
        _animationFrame = -1;
        AdvanceCoroutine();
    }
}
