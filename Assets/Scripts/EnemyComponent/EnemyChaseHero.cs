using UnityEngine;

public class EnemyChaseHero : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private SpriteRenderer spriteRenderer;

    private Enemy enemy;
    private GameObject hero => enemy.heroDetectionComponent.ClosestTarget;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void MoveTowardHero()
    {
        if (hero == null) return;

        Vector2 currentPosition = transform.position;
        Vector2 targetPoint = hero.transform.position;
        Vector2 direction = (targetPoint - currentPosition).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x > 0;

        }
    }
}
