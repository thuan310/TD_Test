using UnityEngine;

public class EnemyChaseWall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Collider2D wallCollider;
    private Enemy enemy;
    [SerializeField] private GameObject wall;
    private SpriteRenderer spriteRenderer;
    public Vector2 closetPoint => wallCollider.ClosestPoint(transform.position);
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (wall == null)
        {
            wall = GameObject.FindWithTag("Wall"); 
        }
        if (wall != null)
        {
            wallCollider = wall.GetComponent<Collider2D>();
        }
    }

    public void MoveTowardWall()
    {
        if (wallCollider == null) return;

        Vector2 currentPosition = transform.position;
        Vector2 direction = (closetPoint - currentPosition).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x > 0;
        }
    }
}
