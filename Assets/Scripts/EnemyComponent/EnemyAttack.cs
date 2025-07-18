using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1.5f;
    public float meleeRange = 1.5f;

    private float cooldownTimer = 0f;
    private Enemy enemy;
    private Detection detection;

    public GameObject Target => detection.ClosestTarget;

    private bool isInAttackState = false;
    public bool CanAttack => cooldownTimer <= 0f && Target != null;


    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        detection = GetComponent<Detection>();
    }

    private void Update()
    {
        if (!isInAttackState) return;

        cooldownTimer -= Time.deltaTime;

        if (CanAttack)
        {
            Attack();
        }
        else
        {
            var animState = enemy.animator.GetCurrentAnimatorStateInfo(0);
            if (animState.IsName("atk") && animState.normalizedTime >= 1f)
            {
                enemy.SetAnimation("idle");
            }
        }

    }

    public void Attack()
    {
        if (!CanAttack) return;

        cooldownTimer = attackCooldown;

        enemy.SetAnimation("atk");
       
    }

    public void DealMeleeDamage()
    {
        var targetStats = Target.GetComponent<Stats>();
        if (targetStats != null)
        {
            int damage = (int)(enemy.statsComponent.Damage);
            targetStats.TakeDamage(damage, gameObject);
        }
    }

    public void ToggleAttack()
    {
        isInAttackState = !isInAttackState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
