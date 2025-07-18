using Unity.VisualScripting;
using UnityEngine;

public class HeroAttack : MonoBehaviour, IAttackComponent
{
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float meleeRange = 1.5f;

    private float cooldownTimer = 0f;
    private Hero hero;
    private Detection detection;

    public GameObject Target => detection.ClosestTarget;

    private bool isInAttackState = false;
    public bool CanAttack => cooldownTimer <= 0f && Target != null;


    private void Awake()
    {
        hero = GetComponent<Hero>();
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
            var animState = hero.animator.GetCurrentAnimatorStateInfo(0);
            if ((animState.IsName("atk") || animState.IsName("atk_alt")) && animState.normalizedTime >= 1f)
            {
                hero.SetAnimation("idle");
            }
        }

    }

    public void Attack()
    {
        if (!CanAttack) return;

        cooldownTimer = attackCooldown;

        float distance = Vector2.Distance(transform.position, Target.transform.position);

        if (distance <= meleeRange)
        {
            hero.SetAnimation("atk_alt");
        }
        else
        {
            hero.SetAnimation("atk");
        }
    }

    public void DealMeleeDamage()
    {
        var targetStats = Target.GetComponent<Stats>();
        if (targetStats != null)
        {
            int damage = (int)(hero.statsComponent.Damage * 1.2);
            targetStats.TakeDamage(damage, gameObject);
        }
    }

    public void ShootArrow()
    {


        if (Target == null || arrowPrefab == null || shootPoint == null) return;

        GameObject arrowObj = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        if (arrow != null)
        {
            int damage = hero.statsComponent.Damage;
            arrow.Launch(Target, damage, gameObject);
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
