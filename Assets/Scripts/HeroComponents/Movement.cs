using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (direction.x != 0)
            {
                spriteRenderer.flipX = direction.x > 0;
            }
        }
    }
}
