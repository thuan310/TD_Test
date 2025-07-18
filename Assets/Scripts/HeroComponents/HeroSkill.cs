using UnityEngine;

public class HeroSkill : MonoBehaviour {
    [SerializeField] private GameObject skillArrowPrefab;
    [SerializeField] private Transform shootPoint;
    public float cooldown = 5f;

    private float cooldownTimer = 0f;
    public bool IsReady => cooldownTimer <= 0f;

    private Hero hero;
    private Detection detection;

    public GameObject Target => detection.ClosestTarget;

    private void Awake()
    {
        hero = GetComponent<Hero>();
        detection = GetComponent<Detection>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public void UseSkill()
    {
        if (!IsReady || skillArrowPrefab == null || shootPoint == null)
            return;

        cooldownTimer = cooldown;

        hero.SetAnimation("skill");


    }

    public void ShootSkillArrow()
    {
        if (Target == null) return;
        GameObject arrowObj = Instantiate(skillArrowPrefab, shootPoint.position, Quaternion.identity);
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        if (arrow != null)
        {
            int damage = Mathf.RoundToInt(hero.statsComponent.Damage * 2f);
            arrow.Launch(Target, damage, gameObject);
        }
    }
    public float CooldownRemaining => Mathf.Max(0, cooldownTimer);
}